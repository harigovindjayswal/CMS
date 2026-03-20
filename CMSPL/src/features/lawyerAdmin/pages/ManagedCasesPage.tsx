import { Button, Paper, Stack, Table, TableBody, TableCell, TableHead, TableRow, Typography } from "@mui/material";
import { Link } from "react-router";
import { useManagedCases } from "../../../lib/hooks/useLawyerAdmin";

export default function ManagedCasesPage() {
  const { list } = useManagedCases();
  const items = list.data ?? [];

  return (
    <Stack gap={2}>
      <Stack direction="row" justifyContent="space-between" alignItems="center">
        <Typography variant="h5" fontWeight="bold">My Cases</Typography>
        <Button variant="contained" component={Link} to="/lawyeradmin/cases/create">
          Create Case
        </Button>
      </Stack>

      <Paper sx={{ p: 2 }}>
        <Table size="small">
          <TableHead>
            <TableRow>
              <TableCell>ID</TableCell>
              <TableCell>Client</TableCell>
              <TableCell>Assigned Lawyer</TableCell>
              <TableCell>Title</TableCell>
              <TableCell>Status</TableCell>
              <TableCell>Stage</TableCell>
              <TableCell align="right">Details</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {items.map((c) => (
              <TableRow key={c.caseId}>
                <TableCell>{c.caseId}</TableCell>
                <TableCell>{c.clientName ?? c.clientId}</TableCell>
                <TableCell>{c.assignedLawyerName ?? c.assignedLawyerUserId ?? "-"}</TableCell>
                <TableCell>{c.title}</TableCell>
                <TableCell>{c.status}</TableCell>
                <TableCell>{c.stage}</TableCell>
                <TableCell align="right">
                  <Button size="small" component={Link} to={`/lawyeradmin/cases/${c.caseId}`}>
                    View
                  </Button>
                </TableCell>
              </TableRow>
            ))}
            {items.length === 0 ? (
              <TableRow>
                <TableCell colSpan={7}>No cases found.</TableCell>
              </TableRow>
            ) : null}
          </TableBody>
        </Table>
      </Paper>
    </Stack>
  );
}

