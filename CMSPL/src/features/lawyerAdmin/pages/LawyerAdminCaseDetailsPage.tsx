import { Divider, Paper, Table, TableBody, TableCell, TableHead, TableRow, Typography, Button } from "@mui/material";
import { useMemo } from "react";
import { useParams } from "react-router";
import { useManagedCases } from "../../../lib/hooks/useLawyerAdmin";

export default function LawyerAdminCaseDetailsPage() {
  const { id } = useParams();
  const caseId = Number(id);
  const cases = useManagedCases();
  const details = cases.detailsQuery(caseId);

  const data = details.data;
  const documents = useMemo(() => data?.documents ?? [], [data]);
  const notes = useMemo(() => data?.notes ?? [], [data]);

  if (details.isLoading) return <Typography>Loading...</Typography>;
  if (!data) return <Typography>Case not found</Typography>;

  return (
    <Paper sx={{ p: 3, borderRadius: 3 }}>
      <Typography variant="h5" fontWeight="bold">
        {data.title}
      </Typography>
      <Typography sx={{ mt: 1 }}>
        {data.caseType} | {data.courtName} | {data.status} ({data.stage})
      </Typography>
      <Divider sx={{ my: 2 }} />

      <Typography variant="h6">Notes</Typography>
      <Table sx={{ mt: 2 }}>
        <TableHead>
          <TableRow>
            <TableCell>When</TableCell>
            <TableCell>By</TableCell>
            <TableCell>Content</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {notes.map((n) => (
            <TableRow key={n.noteId}>
              <TableCell>{n.createdAt ? new Date(n.createdAt).toLocaleString() : "-"}</TableCell>
              <TableCell>{n.userId}</TableCell>
              <TableCell>{n.content}</TableCell>
            </TableRow>
          ))}
          {notes.length === 0 ? (
            <TableRow>
              <TableCell colSpan={3}>No notes.</TableCell>
            </TableRow>
          ) : null}
        </TableBody>
      </Table>

      <Divider sx={{ my: 3 }} />
      <Typography variant="h6">Documents</Typography>
      <Table sx={{ mt: 2 }}>
        <TableHead>
          <TableRow>
            <TableCell>Title</TableCell>
            <TableCell>Category</TableCell>
            <TableCell>Uploaded</TableCell>
            <TableCell>Download</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {documents.map((d) => (
            <TableRow key={d.documentId}>
              <TableCell>{d.title ?? "-"}</TableCell>
              <TableCell>{d.category ?? "-"}</TableCell>
              <TableCell>{d.uploadedAt ? new Date(d.uploadedAt).toLocaleString() : "-"}</TableCell>
              <TableCell>
                <Button
                  size="small"
                  component="a"
                  href={`${import.meta.env.VITE_API_URL}/Documents/${d.documentId}`}
                  target="_blank"
                >
                  Download
                </Button>
              </TableCell>
            </TableRow>
          ))}
          {documents.length === 0 ? (
            <TableRow>
              <TableCell colSpan={4}>No documents.</TableCell>
            </TableRow>
          ) : null}
        </TableBody>
      </Table>
    </Paper>
  );
}

