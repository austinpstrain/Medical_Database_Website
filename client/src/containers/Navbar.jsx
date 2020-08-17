import React from "react"

import AppBar from "@material-ui/core/AppBar"
import Button from "@material-ui/core/Button"
import IconButton from "@material-ui/core/IconButton"
import Link from "@material-ui/core/Link"
import List from "@material-ui/core/List"
import ListItem from "@material-ui/core/ListItem"
import ListItemIcon from "@material-ui/core/ListItemIcon"
import ListItemText from "@material-ui/core/ListItemText"
import SwipeableDrawer from "@material-ui/core/SwipeableDrawer"
import Toolbar from "@material-ui/core/Toolbar"
import Tooltip from "@material-ui/core/Tooltip"
import { withStyles } from "@material-ui/core"
import AccountBoxIcon from "@material-ui/icons/AccountBox"
import DashboardIcon from "@material-ui/icons/Dashboard"
import ExitToAppIcon from "@material-ui/icons/ExitToApp"
import LocationCityIcon from "@material-ui/icons/LocationCity"
import InfoIcon from "@material-ui/icons/Info"
import MenuIcon from "@material-ui/icons/Menu"

import Cookies from 'universal-cookie'

const cookies = new Cookies();

const styles = theme => ({
  drawerList: {
    width: 250
  },
  title: {
    fontFamily: "Arial",
    fontSize: 28
  },
  toolbar: {
    justifyContent: "space-between"
  },
  right: {
    flex: 1,
    display: "flex",
    justifyContent: "flex-end"
  }
});

class Navbar extends React.Component {
  constructor(props) {
    super(props)
    this.state = {
      showDrawer: false
    }

    this.renderLogin = this.renderLogin.bind(this)
    this.renderDrawer = this.renderDrawer.bind(this)
    this.openDrawer = this.openDrawer.bind(this)
    this.closeDrawer = this.closeDrawer.bind(this)
  }

  render() {
    const { classes } = this.props;

    return <div>
      <AppBar position="static">
        <Toolbar className={classes.toolbar}>
          <Link className={classes.title} variant="h1" color="inherit" underline="none" href="/Index">MUICLINIC</Link>
          <div className={classes.right}>
            {this.renderLogin()}
            <IconButton color="inherit" aria-label="Menu" onClick={this.openDrawer}>
              <MenuIcon />
            </IconButton>
          </div>
        </Toolbar>
      </AppBar>
      <SwipeableDrawer anchor="right" open={this.state.showDrawer}
        onOpen={this.openDrawer}
        onClose={this.closeDrawer}>
        {this.renderDrawer()}
      </SwipeableDrawer>
    </div>
  }

  renderLogin() {
    const username = cookies.get('username');
    if (username) {
      return <div>
        <Tooltip title="Me">
          <IconButton color="inherit" component="a" href="/Me">
            <AccountBoxIcon />
          </IconButton>
        </Tooltip>
        <Tooltip title="Logout">
          <IconButton color="inherit" component="a" href="/Logout">
            <ExitToAppIcon />
          </IconButton>
        </Tooltip>
      </div>
    }

    return <Button color="inherit" component="a" href="/Login">Login</Button>;
  }

  renderDrawer() {
    const { classes } = this.props;
    const username = cookies.get('username');
    const access = cookies.get('access');

    return <List className={classes.drawerList}>
      {!username && <ListItem button component="a" href="/Accounts/Create">
        <ListItemIcon><AccountBoxIcon /></ListItemIcon>
        <ListItemText primary="Register" />
      </ListItem>}
      <ListItem button component="a" href="/Locations/Index">
        <ListItemIcon><LocationCityIcon /></ListItemIcon>
        <ListItemText primary="Locations" />
      </ListItem>
      {access >= 1 && <ListItem button component="a" href="/Doctors/Index">
        <ListItemIcon><InfoIcon /></ListItemIcon>
        <ListItemText primary="Doctors" />
      </ListItem>}
      {access >= 2 && <ListItem button component="a" href="/Patients/PatientInfo">
        <ListItemIcon><InfoIcon /></ListItemIcon>
        <ListItemText primary="Patients" />
      </ListItem>}
      {access >= 2 && <ListItem button component="a" href="/Portal/Reports">
        <ListItemIcon><InfoIcon /></ListItemIcon>
        <ListItemText primary="Reports" />
      </ListItem>}
    </List>
  }

  openDrawer() { this.setState({ showDrawer: true }); }
  closeDrawer() { this.setState({ showDrawer: false }); }
}

export default withStyles(styles)(Navbar)