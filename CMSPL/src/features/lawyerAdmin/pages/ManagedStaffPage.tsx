import { Button, Paper, Stack, Table, TableBody, TableCell, TableHead, TableRow, Typography } from "@mui/material";
import { Link } from "react-router";
import { useManagedStaff } from "../../../lib/hooks/useLawyerAdmin";

export default function ManagedStaffPage() {
  const { list } = useManagedStaff();
  const items = list.data ?? [];

  return (
    <Stack gap={2}>
      <Stack direction="row" justifyContent="space-between" alignItems="center">
        <Typography variant="h5" fontWeight="bold">My Staff</Typography>
        <Button variant="contained" component={Link} to="/lawyeradmin/register-staff">
          Register Staff
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
              <TableCell>Lawyer</TableCell>
              <TableCell>Active</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {items.map((s) => (
              <TableRow key={s.staffId}>
                <TableCell>{s.staffId}</TableCell>
                <TableCell>{`${s.firstName ?? ""} ${s.lastName ?? ""}`.trim() || "-"}</TableCell>
                <TableCell>{s.emailId ?? "-"}</TableCell>
                <TableCell>{s.mobileNo ?? "-"}</TableCell>
                <TableCell>{s.lawyerId}</TableCell>
                <TableCell>{s.isActive ? "Yes" : "No"}</TableCell>
              </TableRow>
            ))}
            {items.length === 0 ? (
              <TableRow>
                <TableCell colSpan={6}>No staff found.</TableCell>
              </TableRow>
            ) : null}
          </TableBody>
        </Table>
      </Paper>
    </Stack>
  );
}

