import { Divider, Drawer, List, ListItemButton, ListItemIcon, ListItemText, Tooltip, Box } from "@mui/material";
import { AdminPanelSettings, Assignment, Gavel, Group, LocationCity, Map, PlaylistAddCheck, Work } from "@mui/icons-material";
import { NavLink } from "react-router";
import { useAccount } from "../../lib/hooks/useAccount";
import type { ReactNode } from "react";

export type SidebarMode = "expanded" | "collapsed";
const EXPANDED_WIDTH = 240;
const COLLAPSED_WIDTH = 70;
const APP_BAR_HEIGHT = 64;

type Props = {
  mode: SidebarMode;
  isMobile: boolean;
  mobileOpen: boolean;
  onMobileClose: () => void;
};

type NavItem = {
  to: string;
  label: string;
  icon: ReactNode;
};

function getItems(userType?: string): NavItem[] {
  if (!userType) return [];

  if (userType === "Client") {
    return [
      // { to: "/profile", label: "My Profile", icon: <AccountCircle /> },
      { to: "/request-lawyer", label: "Request Lawyer", icon: <Work /> },
      { to: "/my-requests", label: "My Requests", icon: <Assignment /> },
      { to: "/cases", label: "My Cases", icon: <Gavel /> },
    ];
  }

  if (userType === "Lawyer") {
    return [
      // { to: "/profile", label: "My Profile", icon: <AccountCircle /> },
      { to: "/incoming-requests", label: "Client Requests", icon: <Assignment /> },
      { to: "/cases", label: "Cases", icon: <Gavel /> },
    ];
  }

  if (userType === "Staff") {
    return [
      { to: "/cases", label: "Cases", icon: <Gavel /> },
      { to: "/profile", label: "My Profile", icon: <Assignment /> },
    ];
  }

  if (userType === "LawyerAdmin") {
    return [
      { to: "/lawyeradmin/register-client", label: "Register Client", icon: <Group /> },
      { to: "/lawyeradmin/register-lawyer", label: "Register Lawyer", icon: <Group /> },
      { to: "/lawyeradmin/register-staff", label: "Register Staff", icon: <Group /> },
      { to: "/lawyeradmin/clients", label: "My Clients", icon: <Assignment /> },
      { to: "/lawyeradmin/lawyers", label: "My Lawyers", icon: <Assignment /> },
      { to: "/lawyeradmin/staff", label: "My Staff", icon: <Assignment /> },
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

export default function SideNav({ mode, isMobile, mobileOpen, onMobileClose }: Props) {
  const { currentUser } = useAccount();
  const items = getItems(currentUser?.userType);

  if (!currentUser) return null;

  const drawerWidth = mode === "collapsed" ? COLLAPSED_WIDTH : EXPANDED_WIDTH;

  const content = (
    <Box sx={{ height: "100%", display: "flex", flexDirection: "column" }}>
      {/* <Box
        sx={{
          px: mode === "collapsed" ? 1 : 2,
          py: 2,
          display: "flex",
          alignItems: "center",
          justifyContent: mode === "collapsed" ? "center" : "flex-start",
          gap: 1,
        }}
      >
        <Box
          sx={{
            width: 10,
            height: 10,
            borderRadius: "50%",
            bgcolor: "primary.main",
            boxShadow: "0 0 0 4px rgba(0,0,0,0.03)",
          }}
        />
        {mode === "expanded" ? (
          <Typography fontWeight={700} letterSpacing={0.2}>
            CMS
          </Typography>
        ) : null}
      </Box> */}
      <Divider />
      <List sx={{ py: 1 }}>
        {items.map((item) => {
          const button = (
            <ListItemButton
              component={NavLink}
              to={item.to}
              onClick={() => {
                if (isMobile) onMobileClose();
              }}
              sx={{
                mx: 1,
                my: 0.5,
                borderRadius: 2,
                minHeight: 44,
                justifyContent: mode === "collapsed" ? "center" : "flex-start",
                gap: mode === "collapsed" ? 0 : 1.5,
                "&.active": {
                  bgcolor: "action.selected",
                },
              }}
            >
              <ListItemIcon sx={{ minWidth: mode === "collapsed" ? "auto" : 36 }}>
                {item.icon}
              </ListItemIcon>
              {mode === "expanded" ? <ListItemText primary={item.label} /> : null}
            </ListItemButton>
          );

          return mode === "collapsed" ? (
            <Tooltip key={item.to} title={item.label} placement="right">
              {button}
            </Tooltip>
          ) : (
            <Box key={item.to}>{button}</Box>
          );
        })}
      </List>
    </Box>
  );

  return (
    <>
      {/* Mobile overlay */}
      <Drawer
        variant="temporary"
        open={isMobile && mobileOpen}
        onClose={onMobileClose}
        ModalProps={{ keepMounted: true }}
        sx={{
          display: { xs: "block", sm: "none" },
          [`& .MuiDrawer-paper`]: {
            width: EXPANDED_WIDTH,
            boxSizing: "border-box",
            top: `${APP_BAR_HEIGHT}px`,
            height: `calc(100% - ${APP_BAR_HEIGHT}px)`,
          },
        }}
      >
        {content}
      </Drawer>

      {/* Desktop/tablet */}
      <Drawer
        variant="permanent"
        sx={{
          display: { xs: "none", sm: "block" },
          width: drawerWidth,
          flexShrink: 0,
          [`& .MuiDrawer-paper`]: {
            width: drawerWidth,
            boxSizing: "border-box",
            overflowX: "hidden",
            borderRightColor: "divider",
            top: `${APP_BAR_HEIGHT}px`,
            height: `calc(100% - ${APP_BAR_HEIGHT}px)`,
            transition: (theme) =>
              theme.transitions.create("width", {
                easing: theme.transitions.easing.sharp,
                duration: theme.transitions.duration.shortest,
              }),
          },
        }}
      >
        {content}
      </Drawer>
    </>
  );
}

export function getSidebarWidth(mode: SidebarMode) {
  return mode === "collapsed" ? COLLAPSED_WIDTH : EXPANDED_WIDTH;
}
