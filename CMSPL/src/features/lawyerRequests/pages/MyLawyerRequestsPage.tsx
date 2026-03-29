import { Paper, Typography, Table, TableBody, TableCell, TableHead, TableRow, Button, Box } from "@mui/material";
import { useAccount } from "../../../lib/hooks/useAccount";
import { useLawyerRequests } from "../../../lib/hooks/useLawyerRequests";
import { Link } from "react-router";

export default function MyLawyerRequestsPage() {
  const { currentUser } = useAccount();
  const userType = currentUser?.userType;

  const { listQuery } = useLawyerRequests("client");

  if (userType !== "Client") {
    return (
      <Paper sx={{ p: 3, borderRadius: 3 }}>
        <Typography variant="h6">My requests</Typography>
        <Typography sx={{ mt: 1 }}>This page is available for Client accounts.</Typography>
      </Paper>
    );
  }

  if (listQuery.isLoading) return <Typography>Loading...</Typography>;

  const items = listQuery.data ?? [];

  return (
    <Paper sx={{ p: 3, borderRadius: 3 }}>
      <Typography variant="h5" fontWeight="bold">
        My lawyer requests
      </Typography>
      <Box sx={{ display: "flex", justifyContent: "flex-end", mt: 2 }}>
        <Button component={Link} to="/request-lawyer" variant="contained">
          Request lawyer
        </Button>
      </Box>
      <Table sx={{ mt: 2 }}>
        <TableHead>
          <TableRow>
            <TableCell>Lawyer</TableCell>
            <TableCell>Case type</TableCell>
            <TableCell>Status</TableCell>
            <TableCell>Remark</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {items.map((r) => (
            <TableRow key={r.lawyerRequestId}>
              <TableCell>{r.lawyerName}</TableCell>
              <TableCell>{r.caseTypeName}</TableCell>
              <TableCell>{String(r.statusText)}</TableCell>
              <TableCell>{r.lawyerRemark ?? "-"}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </Paper>
  );
}
