import { useEffect, useState } from "react";
import { useParams } from "react-router";
import { isHtmlElement } from "react-router-dom/dist/dom";
import { useAuth } from "../auth/AuthProvider";
import { SupplierBranchDTO } from "../DTO/SupplierBranchDTO";
import { SupplierQuotesDTO } from "../DTO/SupplierQuotesDTO";
import { useQuote } from "../provider/Quote";

export function Mailto() {
  let { supplier } = useParams();

  const { quote } = useQuote();

  const auth = useAuth();

  const [selected, setSelected] = useState<string[]>([]);
  const [supplierBranch, setSupplierBranch] = useState<SupplierBranchDTO>();

  useEffect(() => {
    auth
      .requestv2(`/api/SupplierBranches/${supplier}`, { method: "GET" })
      .then((res) => {
        setSupplierBranch(res);
      });
  }, []);

  let vals: SupplierQuotesDTO[] = [];

  quote?.items.forEach((item) => {
    item.subquotes.forEach((sqquote) => {
      if (sqquote.supplier_id == supplier && !sqquote.accepted_by_user_id)
        vals.push(sqquote);
    });
  });

  let body = "Good day,\n\nWould you please quote the following items?\n\n";

  vals.forEach((e) => {
    if (selected.includes(e.id)) body += "Part no: " + e.part_number + "\n";
  });

  body += "\nKind regards\nJOB COST PRO Automated quoting system";

  return (
    <>
      <div>
        <table className="border table-auto border-collapse border-black">
          <thead>
            <tr>
              <th className="border px-1 border-black">Supplier</th>
              <th className="border px-1 border-black">Branch</th>
              <th className="border px-1 border-black">Part No</th>
              <th className="border px-1 border-black">Quantity</th>
              <th className="border px-1 border-black">Select</th>
            </tr>
          </thead>
          <tbody>
            {vals.map((val, i) => {
              return (
                <tr key={i}>
                  <td className="border px-1 border-black">
                    {val.supplier.supplier.name}
                  </td>
                  <td className="border px-1 border-black">
                    {val.supplier.name}
                  </td>
                  <td className="border px-1 border-black">
                    {val.part_number}
                  </td>
                  <td className="border px-1 border-black">{val.count}</td>
                  <td className="border px-1 border-black">
                    <input
                      type="checkbox"
                      checked={selected.includes(val.id)}
                      onChange={(e) => {
                        let nselected = [...selected];

                        if (e.target.checked)
                          nselected = [val.id, ...nselected];
                        else
                          nselected = nselected.filter((v) => {
                            if (v != val.id) return val;
                          });

                        setSelected(nselected);
                      }}
                    ></input>
                  </td>
                </tr>
              );
            })}
          </tbody>
        </table>
      </div>
      <div>
        <button
          className="bg-blue-400 rounded-md p-1"
          onClick={() => {
            location.href = `mailto:${
              supplierBranch?.email ?? ""
            }?subject=Quote%20for%20parts&body=${encodeURIComponent(body)}`;
          }}
        >
          Request quote
        </button>
      </div>
    </>
  );
}
