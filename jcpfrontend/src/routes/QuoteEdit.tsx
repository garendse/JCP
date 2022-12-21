import { useEffect, useState } from "react";
import { Outlet, useLocation, useParams } from "react-router";
import { useAuth } from "../auth/AuthProvider";

import save from "../../images/icons/save-solid.svg";
import cancel from "../../images/icons/cancel_FILL0_wght400_GRAD0_opsz48.svg";
import config from "../config";
import { useQuote } from "../provider/Quote";
import { TechDTO } from "../DTO/TechDTO";
import { Loading } from "../components/Loading";
import hourglass from "../../images/icons/hourglass_empty_black_24dp.svg";
import { useCommonProps } from "../provider/CommonProps";

export function QuoteEdit() {
  let params = useParams();
  const auth = useAuth();
  const location = useLocation();

  const {
    quote,
    setQuoteData,
    saveQuote,
    changed,
    saving,
    setChanged,
    setSaving,
  } = useQuote();

  const props = useCommonProps();

  const [error, setError] = useState<string | undefined>(undefined);

  const dateCreated = new Date(
    Date.parse(quote?.create_datetime ?? "")
  ).toString();

  const dateUpdated = new Date(
    Date.parse(quote?.update_datetime ?? quote?.create_datetime ?? "")
  ).toString();

  useEffect(() => {
    auth
      .requestv2(`/api/quotes/${params.quoteId}`, {
        method: "GET",
      })
      .then(
        (res) => {
          setQuoteData(res);
        },
        (err) => {
          setError("Failed to get data from API!\n" + err);
        }
      );
  }, []);

  if (quote?.id != params.quoteId && !error) return <Loading />;
  else
    return (
      <>
        {error && (
          <div className="flex justify-center">
            <h1>{error}</h1>
          </div>
        )}
        <div className="bg-slate-200">
          <p className="text-2xl m-2 mb-0 text-center">
            <b>Ro Number:</b> <span className="px-1">{quote?.ro_number}</span>
            <b>Vehicle:</b>
            <span className="px-1">
              {quote?.vehicle?.brand +
                " " +
                quote?.vehicle?.model +
                " " +
                quote?.vehicle?.year}
            </span>
            <b>{quote?.vehicle?.registration}</b>
          </p>
          <div className="flex flex-wrap justify-center p-4 pt-0 text-xl whitespace-nowrap">
            <p className="m-2">
              <b>Customer Name:</b>{" "}
              {quote?.customer?.title +
                " " +
                quote?.customer?.name +
                " " +
                quote?.customer?.surname}
            </p>
            <p className="m-2">
              <b>Mobile:</b> {quote?.customer?.mobile_no ?? "N/A"}
            </p>
            <p className="m-2">
              <b>Work:</b> {quote?.customer?.work_no ?? "N/A"}
            </p>
            <p className="m-2">
              <b>Alt:</b> {quote?.customer?.alt_no ?? "N/A"}
            </p>
            <p className="m-2">
              <b>Home:</b> {quote?.customer?.home_no ?? "N/A"}
            </p>
            <p className="m-2">
              <b>Email:</b> {quote?.customer?.email ?? "N/A"}
            </p>
            <p className="m-2">
              <b>Status:</b>
              <select
                className="rounded-md ml-2"
                value={quote?.status ?? ""}
                onChange={(e) => {
                  if (quote) setQuoteData({ ...quote, status: e.target.value });

                  setChanged(true);
                }}
              >
                <option value={""}>---</option>
                {props.quoteStatus.map((val, i) => (
                  <option key={i} value={val}>
                    {val}
                  </option>
                ))}
              </select>
            </p>
            <p className="m-2">
              <b>Tech:</b>
              <select
                className="rounded-md ml-2"
                value={quote?.tech_id ?? ""}
                onChange={(e) => {
                  if (quote)
                    setQuoteData({ ...quote, tech_id: e.target.value });

                  setChanged(true);
                }}
              >
                <option value={""}>---</option>
                {props.techs?.map((val, i) => (
                  <option key={i} value={val.id}>
                    {`${val.name} ${val.surname}`}
                  </option>
                ))}
              </select>
            </p>
          </div>
        </div>
        <div className={"flex justify-center" + (saving ? "" : " invisible")}>
          <div>
            <img src={hourglass} className="animate-spin block"></img>
          </div>
          <h1>Saving...</h1>
        </div>
        <div
          className={"flex justify-end px-3" + (changed ? "" : " invisible")}
        >
          <button
            className="mx-2"
            title="Save"
            onClick={() => saveQuote(setError)}
          >
            <img src={save} className="block w-6"></img>
          </button>
          <button
            className="mx-2"
            title="cancel"
            onClick={() => window.location.reload()}
          >
            <img src={cancel} className="block w-6"></img>
          </button>
        </div>
        <span className="text-xs mx-1 text-gray-400">{`Created by ${quote?.create_user?.name} ${quote?.create_user?.surname} on ${dateCreated}`}</span>
        <span className="text-xs mx-1 text-gray-400">{`Updated by ${
          quote?.update_user?.name ?? quote?.create_user?.name
        } ${
          quote?.update_user?.surname ?? quote?.create_user?.surname
        } on ${dateUpdated}`}</span>
        <div className="h-full mb-6">
          <Outlet />
        </div>
      </>
    );
}
