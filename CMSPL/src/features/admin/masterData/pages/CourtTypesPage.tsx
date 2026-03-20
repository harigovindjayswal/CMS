import { Button, Dialog, DialogActions, DialogContent, DialogTitle, FormControlLabel, Paper, Stack, Switch, Table, TableBody, TableCell, TableHead, TableRow, TextField, Typography } from "@mui/material";
import { useState } from "react";
import { toast } from "react-toastify";
import { useCourtTypes } from "../../../../lib/hooks/useAdminMasterData";

type FormState = { courtTypeId: number; typeName: string; isActive: boolean };

export default function CourtTypesPage() {
  const { list, create, update, remove } = useCourtTypes();
  const [open, setOpen] = useState(false);
  const [form, setForm] = useState<FormState>({ courtTypeId: 0, typeName: "", isActive: true });

  const items = list.data ?? [];

  const startCreate = () => {
    setForm({ courtTypeId: 0, typeName: "", isActive: true });
    setOpen(true);
  };

  const startEdit = (item: CourtTypeMst) => {
    setForm({ courtTypeId: item.courtTypeId, typeName: item.typeName, isActive: item.isActive });
    setOpen(true);
  };

  const onSave = async () => {
    if (!form.typeName.trim()) {
      toast.error("Type name is required");
      return;
    }

    if (form.courtTypeId === 0) {
      await create.mutateAsync({ typeName: form.typeName.trim(), isActive: form.isActive });
      toast.success("Court type created");
    } else {
      await update.mutateAsync({ courtTypeId: form.courtTypeId, typeName: form.typeName.trim(), isActive: form.isActive });
      toast.success("Court type updated");
    }
    setOpen(false);
  };

  const onDelete = async (id: number) => {
    await remove.mutateAsync(id);
    toast.success("Court type deleted");
  };

  return (
    <Stack gap={2}>
      <Stack direction="row" justifyContent="space-between" alignItems="center">
        <Typography variant="h5" fontWeight="bold">Court Types</Typography>
        <Button variant="contained" onClick={startCreate}>Add Court Type</Button>
      </Stack>

      <Paper sx={{ p: 2 }}>
        <Table size="small">
          <TableHead>
            <TableRow>
              <TableCell>ID</TableCell>
              <TableCell>Type Name</TableCell>
              <TableCell>Active</TableCell>
              <TableCell align="right">Actions</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {items.map((ct) => (
              <TableRow key={ct.courtTypeId}>
                <TableCell>{ct.courtTypeId}</TableCell>
                <TableCell>{ct.typeName}</TableCell>
                <TableCell>{ct.isActive ? "Yes" : "No"}</TableCell>
                <TableCell align="right">
                  <Stack direction="row" justifyContent="flex-end" gap={1}>
                    <Button size="small" onClick={() => startEdit(ct)}>Edit</Button>
                    <Button size="small" color="error" onClick={() => onDelete(ct.courtTypeId)}>Delete</Button>
                  </Stack>
                </TableCell>
              </TableRow>
            ))}
            {items.length === 0 ? (
              <TableRow>
                <TableCell colSpan={4}>No court types found.</TableCell>
              </TableRow>
            ) : null}
          </TableBody>
        </Table>
      </Paper>

      <Dialog open={open} onClose={() => setOpen(false)} fullWidth maxWidth="sm">
        <DialogTitle>{form.courtTypeId === 0 ? "Create Court Type" : "Edit Court Type"}</DialogTitle>
        <DialogContent sx={{ pt: 2 }}>
          <Stack gap={2} sx={{ mt: 1 }}>
            <TextField
              label="Type Name"
              value={form.typeName}
              onChange={(e) => setForm((p) => ({ ...p, typeName: e.target.value }))}
              fullWidth
            />
            <FormControlLabel
              control={<Switch checked={form.isActive} onChange={(e) => setForm((p) => ({ ...p, isActive: e.target.checked }))} />}
              label="Active"
            />
          </Stack>
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setOpen(false)}>Cancel</Button>
          <Button variant="contained" onClick={onSave} disabled={create.isPending || update.isPending}>
            Save
          </Button>
        </DialogActions>
      </Dialog>
    </Stack>
  );
}

