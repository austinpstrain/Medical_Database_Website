import React, { Suspense } from "react"
import ReactDOM from "react-dom"

import { createMuiTheme, MuiThemeProvider } from "@material-ui/core/styles"
import DefaultTheme from "./themes/Default"

import ContainerNavbar from "./containers/Navbar";

const Navbar = (props) => (
  <MuiThemeProvider theme={DefaultTheme}>
    <ContainerNavbar />
  </MuiThemeProvider>
);

ReactDOM.render(
  <Navbar />,
  document.getElementById("navbar")
);
