const __prod__ = import.meta.env.PROD;

let base_url = import.meta.env.JCP_BASE_URL;
let api_url = import.meta.env.JCP_API_URL;

const config = {
  base_url: base_url,
  api_url: api_url,
  currencySymbol: "R",
  currencySeparator: " ",
  currencyDot: ",",
  VAT: 0.15,
};

export default config;
