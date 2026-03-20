import { Navigate, Outlet, useLocation } from "react-router";
import { Typography } from "@mui/material";
import { useAccount } from "../../lib/hooks/useAccount";

type Props = {
  roles: string[];
};

export default function RequireRole({ roles }: Props) {
  const { currentUser, loadingUserInfo } = useAccount();
  const location = useLocation();

  if (loadingUserInfo) return <Typography>Loading...</Typography>;

  if (!currentUser) {
    return <Navigate to="/login" state={{ from: location }} />;
  }

  const role = currentUser.userType || currentUser.role;
  if (!role || !roles.includes(role)) {
    return <Navigate to="/not-found" replace />;
  }

  return <Outlet />;
}

