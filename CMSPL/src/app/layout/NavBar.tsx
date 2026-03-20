import {
  AppBar,
  Box,
  Toolbar,
  Typography,
  Container,
  MenuItem,
  LinearProgress,
} from "@mui/material";
import { Group } from "@mui/icons-material";
import { NavLink } from "react-router";
import MenuItemLink from "../shared/components/MenuItemLink";
import { useStore } from "../../lib/hooks/useStore";
import { Observer } from "mobx-react-lite";
import { useAccount } from "../../lib/hooks/useAccount";
import UserMenu from "./UserMenu";

export default function NavBar() {
  const { uiStore } = useStore();
  const {currentUser} = useAccount();
  return (
    <Box sx={{ flexGrow: 1 }}>
      <AppBar
        position="static"
        sx={{
          backgroundImage:
            "linear-gradient(135deg, #182a73 0%, #218aae 69%, #207aac 89%)",
          position: "relative",
        }}
      >
        <Container maxWidth="xl">
          <Toolbar sx={{ display: "flex", justifyContent: "space-between" }}>
            <Box>
              <MenuItem
                sx={{ display: "flex", gap: 2 }}
                component={NavLink}
                to="/"
              >
                <Group fontSize="large"></Group>
                <Typography variant="h4" fontWeight="bold">
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
