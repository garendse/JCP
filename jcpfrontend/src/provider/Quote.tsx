import React, { useContext, useState } from "react";
import { useAuth } from "../auth/AuthProvider";
import { QuoteDTO } from "../DTO/QuoteDTO";
import { QuoteItemDTO } from "../DTO/QuoteItemDTO";
import { SupplierQuotesDTO } from "../DTO/SupplierQuotesDTO";

interface QuoteContextType {
  quote: QuoteDTO | undefined;
  setQuoteData: (quote: QuoteDTO) => void;
  saveQuote: (setError: (val: string) => void) => void;
  setSaving: (val: boolean) => void;
  saving: boolean;
  changed: boolean;
  setChanged: (val: boolean) => void;
}

export const QuoteContext = React.createContext<QuoteContextType>(null!);

export function useQuote() {
  return useContext(QuoteContext);
}

export function QuoteProvider({ children }: { children: React.ReactNode }) {
  const [quoteData, setQuoteData] = useState<QuoteDTO>();

  const [changed, setChanged] = useState<boolean>(false);
  const [saving, setSaving] = useState<boolean>(false);

  const auth = useAuth();

  const setQuote = (quote: QuoteDTO) => {
    quote.items = quote.items.sort((a, b) => a.sort_order - b.sort_order);
    setQuoteData(quote);
  };

  const saveSubquotes = async (subquotes: SupplierQuotesDTO[]) => {
    for (let i of subquotes) {
      const { supplier, ...item } = i;
      const id = i?.id ?? "";
      auth.requestv2(`/api/QuoteItemSupplier/${id}`, {
        method: i.id ? "PUT" : "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(item),
      });
    }
  };

  const saveQuoteItems = async (items: QuoteItemDTO[]) => {
    for (let i of items) {
      const { subquotes, ...item } = i;

      await saveSubquotes(i.subquotes);

      auth.requestv2(`/api/QuoteItem/${item?.id}`, {
        method: item.id ? "PUT" : "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(item),
      });
    }
  };

  const saveQuote = async (setError: (val: string) => void) => {
    setChanged(false);
    setSaving(true);

    if (!quoteData) return;

    const {
      vehicle,
      items,
      create_user,
      customer,
      tech,
      update_user,
      ...quote
    } = quoteData;

    await saveQuoteItems(items);

    auth
      .requestv2(`/api/quotes/${quote?.id}`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(quote),
      })
      .then(
        (_) => {
          setSaving(false);
          location.reload();
        },
        (err) => {
          setError("Failed to get data from API!\n" + err);
          setSaving(false);
        }
      );
  };

  let value = {
    quote: quoteData,
    setQuoteData: setQuote,
    saveQuote,
    changed,
    setChanged,
    saving,
    setSaving,
  };

  return (
    <QuoteContext.Provider value={value}>{children}</QuoteContext.Provider>
  );
}
