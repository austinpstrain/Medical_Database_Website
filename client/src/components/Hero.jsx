import React from "react"

import Button from "@material-ui/core/Button"
import Typography from "@material-ui/core/Typography"
import { withStyles } from "@material-ui/core/styles"

import heroImage from "../images/hero_image.jpg"

const styles = theme => ({
  backdrop: {
    position: 'absolute',
    left: 0,
    right: 0,
    top: 0,
    bottom: 0,
    backgroundColor: theme.palette.common.black,
    opacity: 0.5,
    zIndex: -1,
  },
  appointmentButton: {
    width: 240,
    height: 70
  },
  background: {
    position: 'absolute',
    left: 0,
    right: 0,
    top: 0,
    bottom: 0,
    backgroundImage: `url(${heroImage})`,
    backgroundSize: 'cover',
    backgroundRepeat: 'no-repeat',
    zIndex: -2,
  },
  layoutBody: {
    marginTop: theme.spacing.unit * 3,
    marginBottom: theme.spacing.unit * 14,
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
  },
  root: {
    color: theme.palette.common.white,
    position: 'relative',
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center',
    [theme.breakpoints.up('sm')]: {
      height: '80vh',
      minHeight: 500,
      maxHeight: 1300,
    },
  },
});

const Hero = (props) => {
  const { classes } = props;

  return <div className={classes.root}>
      <div className={classes.layoutBody}>
        <Typography color="inherit" align="center" variant="h2" marked="center">
          Clinic Services When You Need It
        </Typography>
        <br />
        <br />
        <Typography color="inherit" align="center" variant="h5" className={classes.h5}>
          Need assistance? Make an appointment now!
        </Typography>
        <br />
        <Button variant="contained" color="primary" className={classes.appointmentButton}>
          Make Appointment
        </Button>
        <div className={classes.backdrop} />
        <div className={classes.background} />
      </div>
  </div>
};

export default withStyles(styles)(Hero);