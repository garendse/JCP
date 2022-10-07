/// <reference types="vite/client" />

interface ImportMetaEnv {
  readonly JCP_MAPBOX_TOKEN: string;
  readonly JCP_BASE_URL: string;
  readonly JCP_API_URL: string;
  // more env variables...
}

interface ImportMeta {
  readonly env: ImportMetaEnv;
}
