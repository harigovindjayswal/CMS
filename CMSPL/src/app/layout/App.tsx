import { Box, Container, CssBaseline, useMediaQuery } from "@mui/material";
import NavBar from "./NavBar";
import { Outlet, useLocation } from "react-router";
import HomePage from "../../features/home/HomePage";
import SideNav, { type SidebarMode } from "./SideNav";
import { useEffect, useState } from "react";
import { useTheme } from "@mui/material/styles";

function App() {
  const location = useLocation();
  const theme = useTheme();
  const isMobile = useMediaQuery(theme.breakpoints.down("sm"));
  const isTablet = useMediaQuery(theme.breakpoints.between("sm", "md"));
  const appBarHeight = 64;

  const storageKey = "cms.sidebar.mode";

  const [mode, setMode] = useState<SidebarMode>(() => {
    const stored = localStorage.getItem(storageKey);
    if (stored === "collapsed" || stored === "expanded") return stored;
    return "expanded";
  });
  const [mobileOpen, setMobileOpen] = useState(false);

  // If user never set a preference, default tablet -> collapsed, desktop -> expanded.
  useEffect(() => {
    const stored = localStorage.getItem(storageKey);
    if (stored === "collapsed" || stored === "expanded") return;
    setMode(isTablet ? "collapsed" : "expanded");
  }, [isTablet]);

  useEffect(() => {
    localStorage.setItem(storageKey, mode);
  }, [mode]);

  // Close overlay when switching to desktop/tablet.
  useEffect(() => {
    if (!isMobile) setMobileOpen(false);
  }, [isMobile]);

  const handleToggleSidebar = () => {
    if (isMobile) {
      setMobileOpen((v) => !v);
      return;
    }
    setMode((v) => (v === "expanded" ? "collapsed" : "expanded"));
  };

  return (
    <>
      <Box sx={{ bgcolor: "background.default", minHeight: "100vh" }}>
        <CssBaseline />
        {location.pathname === "/" ? (
          <HomePage />
        ) : (
          <>
            <NavBar onToggleSidebar={handleToggleSidebar} isMobile={isMobile} sidebarMode={mode} />
            <Box sx={{ display: "flex" }}>
              <SideNav mode={mode} isMobile={isMobile} mobileOpen={mobileOpen} onMobileClose={() => setMobileOpen(false)} />
              <Box
                component="main"
                sx={{
                  flexGrow: 1,
                  minWidth: 0,
                  pt: `${appBarHeight}px`,
                }}
              >
                <Container
                  maxWidth="lg"
                  sx={{
                    py: { xs: 2, sm: 3 },
                    px: { xs: 2, sm: 3 },
                  }}
                >
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
