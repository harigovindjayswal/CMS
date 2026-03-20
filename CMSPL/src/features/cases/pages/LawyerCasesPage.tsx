import { Paper, Table, TableBody, TableCell, TableHead, TableRow, Typography } from "@mui/material";
import { Link } from "react-router";
import { useCases } from "../../../lib/hooks/useCases";

export default function LawyerCasesPage() {
  const { listQuery } = useCases("lawyer");
  if (listQuery.isLoading) return <Typography>Loading...</Typography>;
  const items = listQuery.data ?? [];

  return (
    <Paper sx={{ p: 3, borderRadius: 3 }}>
      <Typography variant="h5" fontWeight="bold">
        My assigned cases
      </Typography>
      <Table sx={{ mt: 2 }}>
        <TableHead>
          <TableRow>
            <TableCell>Title</TableCell>
            <TableCell>Client</TableCell>
            <TableCell>Status</TableCell>
            <TableCell>Stage</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {items.map((c) => (
            <TableRow key={c.caseId} hover component={Link} to={`/cases/${c.caseId}`} sx={{ textDecoration: "none" }}>
              <TableCell>{c.title}</TableCell>
              <TableCell>{c.clientName}</TableCell>
              <TableCell>{c.status}</TableCell>
              <TableCell>{c.stage}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </Paper>
  );
}

