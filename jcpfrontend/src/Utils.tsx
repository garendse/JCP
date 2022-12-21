import config from "./config";

export class JCPError extends Error {
  data: any;
  constructor(msg: string, data?: any) {
    super(msg);

    this.data = data;
    // Set the prototype explicitly.
    Object.setPrototypeOf(this, JCPError.prototype);
  }
}
