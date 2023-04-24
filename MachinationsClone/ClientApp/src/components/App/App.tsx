import React, { FC } from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import './App.css';
import { ClientContextProvider } from '../../client';
import {ConnectionType, NodeType, Props} from '../../common/types';
import { Home } from '../Home';
import { Header } from '../Header';
import { Login } from '../Login';
import { Register } from '../Register';
import { Styled } from './Style';
import {CreateGraph} from "../CreateGraph";
import {Machinations} from "../Machinations";
import {createTheme, ThemeProvider} from "@mui/material";

const theme = createTheme({
  palette: {
    secondary: {
      main: '#fff',
    },
    error: {
      main: '#f44336',
    },
  },
});

export type ElementTypesData = {
  nodeTypes: Record<string, NodeType>;
  connectionTypes: Record<string, ConnectionType>;
  isLoaded: boolean;
}

const elementTypes: ElementTypesData = {
  nodeTypes: {},
  connectionTypes: {},
  isLoaded: false,
};

const ElementTypesContext = React.createContext(elementTypes);
export const ElementTypesContextProvider: FC<Props> = ({ children }) => (
  <ElementTypesContext.Provider value={elementTypes}>{children}</ElementTypesContext.Provider>
);

export const useElementTypes = () => React.useContext(ElementTypesContext);

export function App() {
  return (
    <ClientContextProvider>
      <ElementTypesContextProvider>
        <ThemeProvider theme={theme}>
          <BrowserRouter>
            <Header />
            <Styled.Wrapper>
              <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/graphs" element={<Home />} />
                <Route path="/graphs/create" element={<CreateGraph />} />
                <Route path="/graph/:id" element={<Machinations />} />
                <Route path="/login" element={<Login />} />
                <Route path="/register" element={<Register />} />
              </Routes>
            </Styled.Wrapper>
          </BrowserRouter>
        </ThemeProvider>
      </ElementTypesContextProvider>
    </ClientContextProvider>
  );
}
