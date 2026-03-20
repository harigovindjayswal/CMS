import { Button, Paper, Stack, Table, TableBody, TableCell, TableHead, TableRow, Typography } from "@mui/material";
import { Link } from "react-router";
import { useManagedClients } from "../../../lib/hooks/useLawyerAdmin";

export default function ManagedClientsPage() {
  const { list } = useManagedClients();
  const items = list.data ?? [];

  return (
    <Stack gap={2}>
      <Stack direction="row" justifyContent="space-between" alignItems="center">
        <Typography variant="h5" fontWeight="bold">My Clients</Typography>
        <Button variant="contained" component={Link} to="/lawyeradmin/register-client">
          Register Client
        </Button>
      </Stack>

      <Paper sx={{ p: 2 }}>
        <Table size="small">
          <TableHead>
            <TableRow>
              <TableCell>ID</TableCell>
              <TableCell>Name</TableCell>
              <TableCell>Email</TableCell>
              <TableCell>Mobile</TableCell>
              <TableCell>Active</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {items.map((c) => (
              <TableRow key={c.clientId}>
                <TableCell>{c.clientId}</TableCell>
                <TableCell>{`${c.firstName ?? ""} ${c.lastName ?? ""}`.trim() || "-"}</TableCell>
                <TableCell>{c.emailId ?? "-"}</TableCell>
                <TableCell>{c.mobileNo ?? "-"}</TableCell>
                <TableCell>{c.isActive ? "Yes" : "No"}</TableCell>
              </TableRow>
            ))}
            {items.length === 0 ? (
              <TableRow>
                <TableCell colSpan={5}>No clients found.</TableCell>
              </TableRow>
            ) : null}
          </TableBody>
        </Table>
      </Paper>
    </Stack>
  );
}

