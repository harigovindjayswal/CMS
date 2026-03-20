import {
  Box,
  Button,
  Paper,
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableRow,
  TextField,
  Typography,
} from "@mui/material";
import { useMemo, useState } from "react";
import { useAccount } from "../../../lib/hooks/useAccount";
import { useLawyerRequests } from "../../../lib/hooks/useLawyerRequests";
import { Link } from "react-router";

export default function IncomingLawyerRequestsPage() {
  const { currentUser } = useAccount();
  const userType = currentUser?.userType;

  const { listQuery, respondMutation } = useLawyerRequests("lawyer");
  const [remarksById, setRemarksById] = useState<Record<number, string>>({});

  const items = useMemo(() => listQuery.data ?? [], [listQuery.data]);

  if (userType !== "Lawyer") {
    return (
      <Paper sx={{ p: 3, borderRadius: 3 }}>
        <Typography variant="h6">Incoming requests</Typography>
        <Typography sx={{ mt: 1 }}>This page is available for Lawyer accounts.</Typography>
      </Paper>
    );
  }

  if (listQuery.isLoading) return <Typography>Loading...</Typography>;

  return (
    <Paper sx={{ p: 3, borderRadius: 3 }}>
      <Typography variant="h5" fontWeight="bold">
        Incoming lawyer requests
      </Typography>

      <Table sx={{ mt: 2 }}>
        <TableHead>
          <TableRow>
            <TableCell>Client</TableCell>
            <TableCell>Case type</TableCell>
            <TableCell>Description</TableCell>
            <TableCell>Status</TableCell>
            <TableCell>Remark</TableCell>
            <TableCell align="right">Action</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {items.map((r) => {
            const currentRemark = remarksById[r.lawyerRequestId] ?? "";
            return (
              <TableRow key={r.lawyerRequestId}>
                <TableCell>{r.clientName}</TableCell>
                <TableCell>{r.caseTypeName}</TableCell>
                <TableCell sx={{ maxWidth: 280 }}>
                  <Typography variant="body2" noWrap title={r.caseDescription}>
                    {r.caseDescription}
                  </Typography>
                </TableCell>
                <TableCell>{String(r.status)}</TableCell>
                <TableCell sx={{ minWidth: 260 }}>
                  <TextField
                    value={currentRemark}
                    onChange={(e) =>
                      setRemarksById((prev) => ({
                        ...prev,
                        [r.lawyerRequestId]: e.target.value,
                      }))
                    }
                    size="small"
                    fullWidth
                    placeholder="Mandatory remark"
                  />
                </TableCell>
                <TableCell align="right">
                  <Box sx={{ display: "flex", gap: 1, justifyContent: "flex-end" }}>
                    {Number(r.status) === 3 ? (
                      <Button
                        component={Link}
                        to={`/cases/create/${r.lawyerRequestId}`}
                        variant="contained"
                        color="primary"
                      >
                        Create case
                      </Button>
                    ) : null}
                    <Button
                      variant="contained"
                      color="success"
                      disabled={!currentRemark.trim() || respondMutation.isPending}
                      onClick={() =>
                        respondMutation.mutate({
                          lawyerRequestId: r.lawyerRequestId,
                          status: 3,
                          lawyerRemark: currentRemark,
                        })
                      }
                    >
                      Accept
                    </Button>
                    <Button
                      variant="outlined"
                      color="error"
                      disabled={!currentRemark.trim() || respondMutation.isPending}
                      onClick={() =>
                        respondMutation.mutate({
                          lawyerRequestId: r.lawyerRequestId,
                          status: 4,
                          lawyerRemark: currentRemark,
                        })
                      }
                    >
                      Reject
                    </Button>
                  </Box>
                </TableCell>
              </TableRow>
            );
          })}
        </TableBody>
      </Table>
    </Paper>
  );
}
