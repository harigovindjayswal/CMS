import { Button, Dialog, DialogActions, DialogContent, DialogTitle, Paper, Stack, Switch, FormControlLabel, Table, TableBody, TableCell, TableHead, TableRow, TextField, Typography } from "@mui/material";
import { useState } from "react";
import { useStates } from "../../../../lib/hooks/useAdminMasterData";
import { toast } from "react-toastify";

type FormState = { stateId: number; name: string; isActive: boolean };

export default function StatesPage() {
  const { list, create, update, remove } = useStates();
  const [open, setOpen] = useState(false);
  const [form, setForm] = useState<FormState>({ stateId: 0, name: "", isActive: true });

  const items = list.data ?? [];

  const startCreate = () => {
    setForm({ stateId: 0, name: "", isActive: true });
    setOpen(true);
  };

  const startEdit = (item: StateMst) => {
    setForm({ stateId: item.stateId, name: item.name, isActive: item.isActive });
    setOpen(true);
  };

  const onSave = async () => {
    if (!form.name.trim()) {
      toast.error("Name is required");
      return;
    }

    if (form.stateId === 0) {
      await create.mutateAsync({ name: form.name.trim(), isActive: form.isActive });
      toast.success("State created");
    } else {
      await update.mutateAsync({ stateId: form.stateId, name: form.name.trim(), isActive: form.isActive });
      toast.success("State updated");
    }

    setOpen(false);
  };

  const onDelete = async (id: number) => {
    await remove.mutateAsync(id);
    toast.success("State deleted");
  };

  return (
    <Stack gap={2}>
      <Stack direction="row" justifyContent="space-between" alignItems="center">
        <Typography variant="h5" fontWeight="bold">States</Typography>
        <Button variant="contained" onClick={startCreate}>Add State</Button>
      </Stack>

      <Paper sx={{ p: 2 }}>
        <Table size="small">
          <TableHead>
            <TableRow>
              <TableCell>ID</TableCell>
              <TableCell>Name</TableCell>
              <TableCell>Active</TableCell>
              <TableCell align="right">Actions</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {items.map((s) => (
              <TableRow key={s.stateId}>
                <TableCell>{s.stateId}</TableCell>
                <TableCell>{s.name}</TableCell>
                <TableCell>{s.isActive ? "Yes" : "No"}</TableCell>
                <TableCell align="right">
                  <Stack direction="row" justifyContent="flex-end" gap={1}>
                    <Button size="small" onClick={() => startEdit(s)}>Edit</Button>
                    <Button size="small" color="error" onClick={() => onDelete(s.stateId)}>Delete</Button>
                  </Stack>
                </TableCell>
              </TableRow>
            ))}
            {items.length === 0 ? (
              <TableRow>
                <TableCell colSpan={4}>No states found.</TableCell>
              </TableRow>
            ) : null}
          </TableBody>
        </Table>
      </Paper>

      <Dialog open={open} onClose={() => setOpen(false)} fullWidth maxWidth="sm">
        <DialogTitle>{form.stateId === 0 ? "Create State" : "Edit State"}</DialogTitle>
        <DialogContent sx={{ pt: 2 }}>
          <Stack gap={2} sx={{ mt: 1 }}>
            <TextField
              label="Name"
              value={form.name}
              onChange={(e) => setForm((p) => ({ ...p, name: e.target.value }))}
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

