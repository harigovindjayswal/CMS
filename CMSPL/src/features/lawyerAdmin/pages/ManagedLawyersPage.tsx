import { Button, Paper, Stack, Table, TableBody, TableCell, TableHead, TableRow, Typography } from "@mui/material";
import { Link } from "react-router";
import { useManagedLawyers } from "../../../lib/hooks/useLawyerAdmin";

export default function ManagedLawyersPage() {
  const { list } = useManagedLawyers();
  const items = list.data ?? [];

  return (
    <Stack gap={2}>
      <Stack direction="row" justifyContent="space-between" alignItems="center">
        <Typography variant="h5" fontWeight="bold">My Lawyers</Typography>
        <Button variant="contained" component={Link} to="/lawyeradmin/register-lawyer">
          Register Lawyer
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
              <TableCell>State</TableCell>
              <TableCell>City</TableCell>
              <TableCell>Active</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {items.map((l) => (
              <TableRow key={l.lawyerId}>
                <TableCell>{l.lawyerId}</TableCell>
                <TableCell>{`${l.firstName ?? ""} ${l.lastName ?? ""}`.trim() || "-"}</TableCell>
                <TableCell>{l.emailId ?? "-"}</TableCell>
                <TableCell>{l.mobileNo ?? "-"}</TableCell>
                <TableCell>{l.stateId ?? "-"}</TableCell>
                <TableCell>{l.cityId ?? "-"}</TableCell>
                <TableCell>{l.isActive ? "Yes" : "No"}</TableCell>
              </TableRow>
            ))}
            {items.length === 0 ? (
              <TableRow>
                <TableCell colSpan={7}>No lawyers found.</TableCell>
              </TableRow>
            ) : null}
          </TableBody>
        </Table>
      </Paper>
    </Stack>
  );
}

