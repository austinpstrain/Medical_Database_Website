import React from "react"
import ReactDOM from "react-dom"

import { MuiThemeProvider } from "@material-ui/core/styles"
import DefaultTheme from "./themes/Default"

import Hero from "./components/Hero"
import Navbar from "./containers/Navbar"

const Home = (props) => (
  <MuiThemeProvider theme={DefaultTheme}>
    <Navbar />
    <Hero />
  </MuiThemeProvider>
);

ReactDOM.render(
  <Home />,
  document.getElementById("root")
);
