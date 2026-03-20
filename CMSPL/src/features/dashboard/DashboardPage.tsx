import { Navigate } from "react-router";
import { useAccount } from "../../lib/hooks/useAccount";
import { Typography } from "@mui/material";

export default function DashboardPage() {
  const { currentUser, loadingUserInfo } = useAccount();

  if (loadingUserInfo) return <Typography>Loading...</Typography>;
  if (!currentUser) return <Navigate to="/login" replace />;

  switch (currentUser.userType) {
    case "Client":
      return <Navigate to="/request-lawyer" replace />;
    case "Lawyer":
      return <Navigate to="/incoming-requests" replace />;
    case "LawyerAdmin":
      return <Navigate to="/lawyeradmin/clients" replace />;
    case "Admin":
      return <Navigate to="/admin/master/states" replace />;
    default:
      return <Navigate to="/profile" replace />;
  }
}

