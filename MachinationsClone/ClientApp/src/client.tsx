import React, { FC } from 'react';
import {Props} from "./common/types";
import {Auth} from "./localStorage.ts";

class Client {
  host: string
  headers: Headers

  constructor(host: string) {
    this.host = host;
    this.headers = new Headers();
    this.headers.append('Content-Type', 'application/json');
  }
  
  setHeader(key: string, value: string) {
    this.headers.set(key, value);
  }

  removeHeader(key: string) {
    this.headers.delete(key);
  }
  
  getHeader(key: string) {
    return this.headers.get(key);
  }

  getHeaders() {
    if (Auth.isLoggedIn())
      this.headers.set('Authorization', `Bearer ${Auth.getToken()}`);
    return this.headers;
  }
}

const client = new Client('http://localhost:5000/api');

export const ClientContext = React.createContext(client);

export const ClientContextProvider: FC<Props> =
  ({ children }) => (
  <ClientContext.Provider value={client}>
    {children}
  </ClientContext.Provider>
);

export const useClient = () => React.useContext(ClientContext);
