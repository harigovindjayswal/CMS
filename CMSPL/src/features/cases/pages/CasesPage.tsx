import { Paper, Typography } from "@mui/material";
import { useAccount } from "../../../lib/hooks/useAccount";
import LawyerCasesPage from "./LawyerCasesPage";
import ClientCasesPage from "./ClientCasesPage";
import StaffCasesPage from "./StaffCasesPage";

export default function CasesPage() {
  const { currentUser } = useAccount();
  const userType = currentUser?.userType;

  if (!userType) return <Typography>Loading...</Typography>;
  if (userType === "Lawyer") return <LawyerCasesPage />;
  if (userType === "Client") return <ClientCasesPage />;
  if (userType === "Staff") return <StaffCasesPage />;

  return (
    <Paper sx={{ p: 3, borderRadius: 3 }}>
      <Typography variant="h6">Cases</Typography>
      <Typography sx={{ mt: 1 }}>Cases UI is currently implemented for Client, Lawyer, and Staff roles.</Typography>
    </Paper>
  );
}
