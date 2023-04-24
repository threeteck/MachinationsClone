import {Button, ButtonProps, createTheme, ThemeProvider} from "@mui/material";
import {red} from "@mui/material/colors";

const theme = createTheme({
  palette: {
    primary: {
      main: red[500]
    }
  }
});

export const DeleteButton = (props: ButtonProps) => {

  return (
    <ThemeProvider  theme={theme}>
      <Button {...props} color="primary"/>
    </ThemeProvider >
  );
}
