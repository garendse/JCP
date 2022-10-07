import React, { useContext, useState } from "react";
import { useAuth } from "../auth/AuthProvider";
import { QuoteDTO } from "../DTO/QuoteDTO";

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

  const saveQuote = (setError: (val: string) => void) => {
    setChanged(false);
    setSaving(true);

    auth
      .requestv2(`/api/quotes/${quoteData?.id}`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(quoteData),
      })
      .then(
        (res) => {
          let q: QuoteDTO = res;

          setQuoteData(q);
          setSaving(false);
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
