import { Divider, Drawer, List, ListItemButton, ListItemIcon, ListItemText, Toolbar } from "@mui/material";
import { AccountCircle, AdminPanelSettings, Assignment, Gavel, Group, LocationCity, Map, PlaylistAddCheck, Work } from "@mui/icons-material";
import { NavLink } from "react-router";
import { useAccount } from "../../lib/hooks/useAccount";
import type { ReactNode } from "react";

const drawerWidth = 260;

type NavItem = {
  to: string;
  label: string;
  icon: ReactNode;
};

function getItems(userType?: string): NavItem[] {
  if (!userType) return [];

  if (userType === "Client") {
    return [
      { to: "/profile", label: "My Profile", icon: <AccountCircle /> },
      { to: "/request-lawyer", label: "Request Lawyer", icon: <Work /> },
      { to: "/my-requests", label: "My Requests", icon: <Assignment /> },
      { to: "/cases", label: "My Cases", icon: <Gavel /> },
    ];
  }

  if (userType === "Lawyer") {
    return [
      { to: "/profile", label: "My Profile", icon: <AccountCircle /> },
      { to: "/incoming-requests", label: "Client Requests", icon: <Assignment /> },
      { to: "/cases", label: "Cases", icon: <Gavel /> },
    ];
  }

  if (userType === "LawyerAdmin") {
    return [
      { to: "/lawyeradmin/register-client", label: "Register Client", icon: <Group /> },
      { to: "/lawyeradmin/register-lawyer", label: "Register Lawyer", icon: <Group /> },
      { to: "/lawyeradmin/clients", label: "My Clients", icon: <Assignment /> },
      { to: "/lawyeradmin/lawyers", label: "My Lawyers", icon: <Assignment /> },
      { to: "/lawyeradmin/cases", label: "My Cases", icon: <Gavel /> },
      { to: "/lawyeradmin/cases/create", label: "Create Case", icon: <PlaylistAddCheck /> },
    ];
  }

  if (userType === "Admin") {
    return [
      { to: "/admin/master/states", label: "States", icon: <Map /> },
      { to: "/admin/master/districts", label: "Districts", icon: <Map /> },
      { to: "/admin/master/cities", label: "Cities", icon: <LocationCity /> },
      { to: "/admin/master/case-types", label: "Case Types", icon: <AdminPanelSettings /> },
      { to: "/admin/master/court-types", label: "Court Types", icon: <AdminPanelSettings /> },
      { to: "/admin/master/courts", label: "Courts", icon: <AdminPanelSettings /> },
    ];
  }

  return [];
}

export default function SideNav() {
  const { currentUser } = useAccount();
  const items = getItems(currentUser?.userType);

  if (!currentUser) return null;

  return (
    <Drawer
      variant="permanent"
      sx={{
        width: drawerWidth,
        flexShrink: 0,
        [`& .MuiDrawer-paper`]: { width: drawerWidth, boxSizing: "border-box" },
      }}
    >
      <Toolbar />
      <Divider />
      <List>
        {items.map((item) => (
          <ListItemButton key={item.to} component={NavLink} to={item.to}>
            <ListItemIcon>{item.icon}</ListItemIcon>
            <ListItemText primary={item.label} />
          </ListItemButton>
        ))}
      </List>
    </Drawer>
  );
}

export { drawerWidth };
