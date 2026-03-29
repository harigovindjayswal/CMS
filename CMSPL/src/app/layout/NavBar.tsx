import {
  AppBar,
  Box,
  Toolbar,
  Typography,
  Container,
  MenuItem,
  LinearProgress,
  IconButton,
} from "@mui/material";
import { Group, Menu as MenuIcon, MenuOpen } from "@mui/icons-material";
import { NavLink } from "react-router";
import MenuItemLink from "../shared/components/MenuItemLink";
import { useStore } from "../../lib/hooks/useStore";
import { Observer } from "mobx-react-lite";
import { useAccount } from "../../lib/hooks/useAccount";
import UserMenu from "./UserMenu";
import type { SidebarMode } from "./SideNav";

export default function NavBar({
  onToggleSidebar,
  isMobile,
  sidebarMode,
}: {
  onToggleSidebar: () => void;
  isMobile: boolean;
  sidebarMode: SidebarMode;
}) {
  const { uiStore } = useStore();
  const {currentUser} = useAccount();
  return (
    <Box sx={{ flexGrow: 1 }}>
      <AppBar
        position="fixed"
        sx={{
          zIndex: (t) => t.zIndex.drawer + 1,
          height: 64,
          bgcolor: "background.paper",
          color: "text.primary",
          borderBottom: "1px solid",
          borderColor: "divider",
          boxShadow: "0 6px 24px rgba(26, 23, 23, 0.06)",
        }}
      >
        <Container maxWidth="xl">
          <Toolbar sx={{ display: "flex", justifyContent: "space-between", gap: 2, minHeight: 64 }}>
            <Box sx={{ display: "flex", alignItems: "center", gap: 1 }}>
              {currentUser ? (
                <IconButton
                  onClick={onToggleSidebar}
                  aria-label={isMobile ? "Open menu" : "Toggle sidebar"}
                  edge="start"
                  size="large"
                  sx={{ mr: 0.5 }}
                >
                  {isMobile ? <MenuIcon /> : sidebarMode === "collapsed" ? <MenuIcon /> : <MenuOpen />}
                </IconButton>
              ) : null}
              <MenuItem
                sx={{ display: "flex", gap: 1.25, borderRadius: 2 }}
                component={NavLink}
                to="/"
              >
                <Group fontSize="large" color="primary" />
                <Typography variant="h6" fontWeight={800} letterSpacing={0.3}>
                  CMS
                </Typography>
              </MenuItem>
            </Box>
            <Box sx={{ display: "flex" }}>
              {currentUser ? <MenuItemLink to="/dashboard">Dashboard</MenuItemLink> : null}
              {currentUser?.userType === "Admin" ? (
                <>
                  <MenuItemLink to="/errors">Errors</MenuItemLink>
                  <MenuItemLink to="/counter">Counter</MenuItemLink>
                </>
              ) : null}
            </Box>
            <Box display='flex' alignItems='center'>
                            {currentUser ? (
                                <UserMenu />
                            ) : (
                                 <>
                                    <MenuItemLink to='/login'>Login</MenuItemLink>
                                    <MenuItemLink to='/register'>Register</MenuItemLink>
                                </>
                            )}
                        </Box>
          </Toolbar>
        </Container>
        <Observer>
          {() =>
            uiStore.isLoading ? (
              <LinearProgress
                color="secondary"
                sx={{
                  position: "absolute",
                  height: 4,
                  left: 0,
                  right: 0,
                  bottom: 0,
                }}
              />
            ) : null
          }
        </Observer>
      </AppBar>
    </Box>
  );
}
