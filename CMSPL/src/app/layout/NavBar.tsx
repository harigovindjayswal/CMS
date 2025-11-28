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

export default function NavBar() {
  const { uiStore } = useStore();
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
              <MenuItemLink to="/clients">Clients</MenuItemLink>
              <MenuItemLink to="/createClient">Create Client</MenuItemLink>
              <MenuItemLink to="/counter">Counter</MenuItemLink>
            </Box>
            <MenuItem>User Manual</MenuItem>
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
