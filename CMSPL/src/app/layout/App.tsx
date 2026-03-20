import { Box, Container, CssBaseline } from "@mui/material";
import NavBar from "./NavBar";
import { Outlet, useLocation } from "react-router";
import HomePage from "../../features/home/HomePage";
import SideNav from "./SideNav";

function App() {
  const location = useLocation();
  return (
    <>
      <Box sx={{ bgcolor: "#eeeeee", minHeight: "100vh" }}>
        <CssBaseline />
        {location.pathname === "/" ? (
          <HomePage />
        ) : (
          <>
            <NavBar />
            <Box sx={{ display: "flex" }}>
              <SideNav />
              <Box component="main" sx={{ flexGrow: 1 }}>
                <Container maxWidth="xl" sx={{ mt: 3 }}>
                  <Outlet />
                </Container>
              </Box>
            </Box>
          </>
        )}
      </Box>
    </>
  );
}

export default App;
