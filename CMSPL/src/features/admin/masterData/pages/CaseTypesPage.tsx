import { Button, Dialog, DialogActions, DialogContent, DialogTitle, FormControlLabel, Paper, Stack, Switch, Table, TableBody, TableCell, TableHead, TableRow, TextField, Typography } from "@mui/material";
import { useState } from "react";
import { toast } from "react-toastify";
import { useCaseTypes } from "../../../../lib/hooks/useAdminMasterData";

type FormState = { caseTypeId: number; typeName: string; isActive: boolean };

export default function CaseTypesPage() {
  const { list, create, update, remove } = useCaseTypes();
  const [open, setOpen] = useState(false);
  const [form, setForm] = useState<FormState>({ caseTypeId: 0, typeName: "", isActive: true });

  const items = list.data ?? [];

  const startCreate = () => {
    setForm({ caseTypeId: 0, typeName: "", isActive: true });
    setOpen(true);
  };

  const startEdit = (item: CaseTypeMst) => {
    setForm({ caseTypeId: item.caseTypeId, typeName: item.typeName, isActive: item.isActive });
    setOpen(true);
  };

  const onSave = async () => {
    if (!form.typeName.trim()) {
      toast.error("Type name is required");
      return;
    }

    if (form.caseTypeId === 0) {
      await create.mutateAsync({ typeName: form.typeName.trim(), isActive: form.isActive });
      toast.success("Case type created");
    } else {
      await update.mutateAsync({ caseTypeId: form.caseTypeId, typeName: form.typeName.trim(), isActive: form.isActive });
      toast.success("Case type updated");
    }
    setOpen(false);
  };

  const onDelete = async (id: number) => {
    await remove.mutateAsync(id);
    toast.success("Case type deleted");
  };

  return (
    <Stack gap={2}>
      <Stack direction="row" justifyContent="space-between" alignItems="center">
        <Typography variant="h5" fontWeight="bold">Case Types</Typography>
        <Button variant="contained" onClick={startCreate}>Add Case Type</Button>
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
              <TableRow key={ct.caseTypeId}>
                <TableCell>{ct.caseTypeId}</TableCell>
                <TableCell>{ct.typeName}</TableCell>
                <TableCell>{ct.isActive ? "Yes" : "No"}</TableCell>
                <TableCell align="right">
                  <Stack direction="row" justifyContent="flex-end" gap={1}>
                    <Button size="small" onClick={() => startEdit(ct)}>Edit</Button>
                    <Button size="small" color="error" onClick={() => onDelete(ct.caseTypeId)}>Delete</Button>
                  </Stack>
                </TableCell>
              </TableRow>
            ))}
            {items.length === 0 ? (
              <TableRow>
                <TableCell colSpan={4}>No case types found.</TableCell>
              </TableRow>
            ) : null}
          </TableBody>
        </Table>
      </Paper>

      <Dialog open={open} onClose={() => setOpen(false)} fullWidth maxWidth="sm">
        <DialogTitle>{form.caseTypeId === 0 ? "Create Case Type" : "Edit Case Type"}</DialogTitle>
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

