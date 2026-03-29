import {
  Box,
  Button,
  Divider,
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
import { useParams } from "react-router";
import { useAccount } from "../../../lib/hooks/useAccount";
import { useCases } from "../../../lib/hooks/useCases";

export default function CaseDetailsPage() {
  const { id } = useParams();
  const caseId = Number(id);
  const { currentUser } = useAccount();
  const userType = currentUser?.userType;

  const cases = useCases(userType === "Lawyer" ? "lawyer" : userType === "Staff" ? "staff" : "client");
  const details = cases.detailsQuery(caseId);

  const [note, setNote] = useState("");
  const [docTitle, setDocTitle] = useState("");
  const [docCategory, setDocCategory] = useState("");
  const [file, setFile] = useState<File | null>(null);

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
      <Box sx={{ display: "flex", gap: 1, mt: 1 }}>
        <TextField value={note} onChange={(e) => setNote(e.target.value)} fullWidth size="small" placeholder="Add a note" />
        <Button
          variant="contained"
          disabled={!note.trim() || cases.addNote.isPending}
          onClick={() => cases.addNote.mutate({ caseId: data.caseId, content: note, isPrivate: false })}
        >
          Add
        </Button>
      </Box>
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
        </TableBody>
      </Table>

      <Divider sx={{ my: 3 }} />
      <Typography variant="h6">Documents</Typography>
      {userType === "Lawyer" || userType === "Staff" ? (
        <Box sx={{ display: "grid", gridTemplateColumns: "1fr 1fr 1fr", gap: 1, mt: 1 }}>
          <TextField value={docTitle} onChange={(e) => setDocTitle(e.target.value)} size="small" placeholder="Title" />
          <TextField value={docCategory} onChange={(e) => setDocCategory(e.target.value)} size="small" placeholder="Category" />
          <Button component="label" variant="outlined">
            Choose file
            <input
              type="file"
              hidden
              onChange={(e) => setFile(e.target.files?.[0] ?? null)}
            />
          </Button>
          <Button
            variant="contained"
            disabled={!file || cases.uploadDocument.isPending}
            onClick={() => {
              if (!file) return;
              cases.uploadDocument.mutate({ caseId: data.caseId, file, title: docTitle, category: docCategory });
            }}
          >
            Upload
          </Button>
          <Typography sx={{ gridColumn: "span 2" }} variant="body2">
            {file ? file.name : "No file selected"}
          </Typography>
        </Box>
      ) : null}

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
        </TableBody>
      </Table>
    </Paper>
  );
}
