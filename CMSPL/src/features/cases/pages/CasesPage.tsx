import { Paper, Typography } from "@mui/material";
import { useAccount } from "../../../lib/hooks/useAccount";
import LawyerCasesPage from "./LawyerCasesPage";
import ClientCasesPage from "./ClientCasesPage";

export default function CasesPage() {
  const { currentUser } = useAccount();
  const userType = currentUser?.userType;

  if (!userType) return <Typography>Loading...</Typography>;
  if (userType === "Lawyer") return <LawyerCasesPage />;
  if (userType === "Client") return <ClientCasesPage />;

  return (
    <Paper sx={{ p: 3, borderRadius: 3 }}>
      <Typography variant="h6">Cases</Typography>
      <Typography sx={{ mt: 1 }}>Cases UI is currently implemented for Client and Lawyer roles.</Typography>
    </Paper>
  );
}

