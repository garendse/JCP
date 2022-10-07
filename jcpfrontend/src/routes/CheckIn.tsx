import { useEffect, useState } from "react";
import { useAuth } from "../auth/AuthProvider";
import {
  CustomerInfoEditBlock,
  CustomerInfoShowBlock,
} from "../components/CustomerInfoBlock";
import { SearchBox } from "../components/Searchbox";
import hourglass from "../../images/icons/hourglass_empty_black_24dp.svg";

import {
  VehicleInfoEditBlock,
  VehicleInfoViewBlock,
} from "../components/VehicleInfoBlock";
import { useNavigate } from "react-router";
import { CustomerDTO } from "../DTO/CustomerDTO";
import { QuoteDTO } from "../DTO/QuoteDTO";
import { VehicleDTO } from "../DTO/VehicleDTO";

const NUM_NEEDED_FOR_AUTOCOMPLETE = 3;
const NUM_NEEDED_FOR_CREATE = 4;

export function CheckIn() {
  const [customerSearch, setCustomerSearch] = useState<string>("");
  const [customers, setCustomers] = useState<CustomerDTO[] | null>(null);
  const [customerInfo, setCustomerInfo] = useState<CustomerDTO | null>(null);

  const [vehicles, setVehicles] = useState<VehicleDTO[] | null>(null);
  const [vehicleInfo, setVehicleInfo] = useState<VehicleDTO | null>(null);
  const [vehicleSelected, setVehicleSelected] = useState<string>("select");

  const [odometer, setOdometer] = useState<number | null>(0);
  const [ro, setRo] = useState<string>("");
  const [ok, setOk] = useState<boolean>(false);

  const [finding, setFinding] = useState<boolean>(false);
  const [customerFinal, setCustomerFinal] = useState<boolean>(false);

  let navigate = useNavigate();

  const auth = useAuth();

  useEffect(() => {
    if (customerSearch.length >= NUM_NEEDED_FOR_AUTOCOMPLETE)
      auth
        .requestv2(`/api/Customers?mobile_no=${customerSearch}&limit=10`, {
          method: "GET",
        })
        .then((res) => setCustomers(res))
        .catch((err) => setCustomers(null));
  }, [customerSearch]);

  useEffect(() => {
    auth
      .requestv2(`/api/Vehicles?customer_id=${customerInfo?.id}`, {
        method: "GET",
      })
      .then((res) => setVehicles(res))
      .catch((err) => setVehicles(null));
  }, [customerFinal]);

  useEffect(() => {
    if (
      vehicleSelected != "Add" &&
      vehicleSelected != "select" &&
      vehicles != null
    ) {
      setOk(true);
      setVehicleInfo(vehicles[parseInt(vehicleSelected)]);
    } else if (vehicleSelected == "Add") {
      setVehicleInfo({
        brand: "",
        customer_id: customerInfo?.id ?? "",
        engine_number: "",
        id: "",
        model: "",
        registration: "",
        vin_number: "",
        year: 0,
      });
    } else setVehicleInfo(null);
  }, [vehicleSelected]);

  const addButtonHandler = () => {
    if (!finding) {
      setFinding(true);

      auth
        .requestv2(`/api/Customers?mobile_no=${customerSearch}&limit=2`, {
          method: "GET",
        })
        .then((res) => {
          let mobile_no = "";
          let company_name = "";
          if (customerSearch.match(/\D/)) {
            company_name = customerSearch;
          } else {
            mobile_no = customerSearch;
          }

          if (res.length == 1) setCustomerInfo(res[0]);
          else {
            let cinfo: CustomerDTO = {
              email: "",
              id: "",
              mobile_no: mobile_no,
              alt_no: "",
              home_no: "",
              work_no: "",
              name: "",
              surname: "",
              title: "",
              address_line_1: "",
              address_line_2: "",
              address_line_3: "",
              company_name: company_name,
              postal: "",
              reg_number: "",
              type: company_name !== "" ? "CORPORATE" : "INDIVIDUAL",
              vat_no: "",
            };

            setCustomerInfo(cinfo);
            setFinding(false);
          }
        })
        .catch((err) => {
          alert("Invalid request! Could not lookup!" + err);
        })
        .finally(() => {
          setFinding(false);
        });
    }
  };

  const onCustomerSave = (data: CustomerDTO, changed: boolean) => {
    setCustomerFinal(true);
    setCustomerInfo(data);

    if (!changed) return;
    setFinding(true);

    if (data.id) {
      auth
        .requestv2(`/api/Customers/${data.id}`, {
          method: "PUT",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(data),
        })
        .then((res) => {
          setCustomerInfo(res);
          setFinding(false);
        })
        .catch((err) => {
          alert("Could not save customer: server error!" + err);
        })
        .finally(() => setFinding(false));
    } else {
      auth
        .requestv2("/api/Customers", {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(data),
        })
        .then((res) => {
          setCustomerInfo(res);
        })
        .catch((err) => alert("Could not save customer: server error!" + err))
        .finally(() => setFinding(false));
    }
  };

  const onVehicleSave = (data: VehicleDTO) => {
    setOk(false);
    setFinding(true);

    auth
      .requestv2(`/api/vehicles`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(data),
      })
      .then((res) => {
        vehicles?.push(res);
        if (vehicles) setVehicleSelected((vehicles.length - 1).toString());
        setOk(true);
      })
      .catch((err) => alert("Could not save vehicle" + err))
      .finally(() => setFinding(false));
  };

  const onQuoteSave = () => {
    setFinding(true);
    var new_quote: QuoteDTO = {
      customer_id: customerInfo?.id ?? "",
      branch_id: "",
      status: "Awaiting Job Card",
      ro_number: ro,
      vehicle_id: vehicleInfo?.id ?? "",
      id: "",
      create_datetime: new Date().toISOString(),
      create_user_id: auth.user.id,
      update_user_id: auth.user.id,
      update_datetime: new Date().toISOString(),
      items: [],
    };

    auth
      .requestv2("/api/Quotes", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(new_quote),
      })
      .then((res) => {
        if (res?.id) navigate(`/quote/${res.id}`, { replace: true });
      })
      .catch((err) => {
        alert("Could not create Quote!\n" + err);
      });
  };

  if (finding)
    return (
      <div className="flex justify-center mb-6 animate-spin">
        <img srcSet={hourglass} />
      </div>
    );

  // @ts-ignore
  let autocomplete: string[] = (
    customerSearch.match(/(\D)+/gm)
      ? customers?.map((val) => {
          if (val.company_name) return val.company_name;
        })
      : customers?.map((val) => {
          if (val.mobile_no) return val.mobile_no;
        })!
  )!;

  return (
    <>
      <div className="mx-2 mt-2 bg-slate-200 rounded-md">
        <h1 className="text-lg font-bold mx-2">Customer</h1>
        {!customerInfo && (
          <>
            <SearchBox
              id="customer"
              onChange={(e) => {
                setCustomerSearch(e.target.value);
              }}
              value={customerSearch}
              autoComplete={autocomplete}
            />
            {customerSearch.length >= NUM_NEEDED_FOR_CREATE && !finding && (
              <>
                <button
                  id="findButton"
                  className="mb-3 mx-3 p-1 px-3 bg-blue-300 rounded-md"
                  onClick={addButtonHandler}
                >
                  Find
                </button>
              </>
            )}
          </>
        )}
        {customerInfo != null && !customerFinal && (
          <CustomerInfoEditBlock
            customer={customerInfo}
            onCancel={() => {
              setCustomerInfo(null);
              setFinding(false);
            }}
            onSave={onCustomerSave}
          />
        )}
        {customerFinal && customerInfo != null && (
          <CustomerInfoShowBlock customer={customerInfo} />
        )}
      </div>
      {customerFinal && customerInfo != null && (
        <div className="mx-2 mt-2 bg-slate-200 rounded-md">
          <h1 className="text-lg font-bold mx-2">Vehicle</h1>
          <select
            className="mx-2 mb-2 px-3 w-auto rounded-md"
            value={vehicleSelected}
            onChange={(e) => setVehicleSelected(e.target.value)}
          >
            <option value="select">---select---</option>
            <option value="Add">Add New</option>
            {vehicles?.map((val, i) => (
              <option key={i} value={i}>
                {val.registration} - {val.brand} {val.model} {val.year} (
                {val.vin_number})
              </option>
            ))}
          </select>
          {vehicleInfo != null && vehicleSelected != "Add" && (
            <>
              <VehicleInfoViewBlock vehicle={vehicleInfo} />
              <div className="mx-3 -mt-2">
                <label htmlFor="odometer">Odometer:</label>
                <input
                  id="odometer"
                  className="m-2 rounded-md w-72"
                  value={odometer ?? ""}
                  onChange={(e) => {
                    setOdometer(
                      !isNaN(parseInt(e.target.value))
                        ? parseInt(e.target.value)
                        : null
                    );
                  }}
                  autoComplete="off"
                  type="number"
                  required={true}
                ></input>
              </div>
            </>
          )}
          {vehicleInfo != null && vehicleSelected == "Add" && (
            <VehicleInfoEditBlock
              vehicle={vehicleInfo}
              onSave={onVehicleSave}
            />
          )}
          {(odometer ?? 0) > 0 && ok && (
            <>
              <div className="mx-3 -mt-2">
                <label htmlFor="odometer">Ro Number:</label>
                <input
                  id="odometer"
                  className="m-2 rounded-md w-72"
                  value={ro}
                  onChange={(e) => {
                    setRo(e.target.value);
                  }}
                  autoComplete="off"
                  type="number"
                  required={true}
                ></input>
              </div>

              <button
                className="bg-green-300 p-1 hover:bg-green-200 rounded-md mx-3 mb-2"
                type="button"
                onClick={onQuoteSave}
              >
                Create New Quote
              </button>
            </>
          )}
        </div>
      )}
    </>
  );
}
