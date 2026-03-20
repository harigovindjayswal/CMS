import { Button, Dialog, DialogActions, DialogContent, DialogTitle, FormControlLabel, MenuItem, Paper, Stack, Switch, Table, TableBody, TableCell, TableHead, TableRow, TextField, Typography } from "@mui/material";
import { useState } from "react";
import { toast } from "react-toastify";
import { useDistricts } from "../../../../lib/hooks/useAdminMasterData";
import { useUtility } from "../../../../lib/hooks/useUtility";

type FormState = { districtId: number; stateId: number; name: string; isActive: boolean };

export default function DistrictsPage() {
  const { list, create, update, remove } = useDistricts();
  const { optionLoader: states } = useUtility("State");
  const [open, setOpen] = useState(false);
  const [form, setForm] = useState<FormState>({ districtId: 0, stateId: 0, name: "", isActive: true });

  const items = list.data ?? [];
  const stateNameById = (id: number) => states.find((s) => Number(s.id) === id)?.name ?? String(id);

  const startCreate = () => {
    setForm({ districtId: 0, stateId: 0, name: "", isActive: true });
    setOpen(true);
  };

  const startEdit = (item: DistrictMst) => {
    setForm({ districtId: item.districtId, stateId: item.stateId, name: item.name, isActive: item.isActive });
    setOpen(true);
  };

  const onSave = async () => {
    if (!form.stateId) {
      toast.error("State is required");
      return;
    }
    if (!form.name.trim()) {
      toast.error("Name is required");
      return;
    }

    if (form.districtId === 0) {
      await create.mutateAsync({ stateId: form.stateId, name: form.name.trim(), isActive: form.isActive });
      toast.success("District created");
    } else {
      await update.mutateAsync({ districtId: form.districtId, stateId: form.stateId, name: form.name.trim(), isActive: form.isActive });
      toast.success("District updated");
    }
    setOpen(false);
  };

  const onDelete = async (id: number) => {
    await remove.mutateAsync(id);
    toast.success("District deleted");
  };

  return (
    <Stack gap={2}>
      <Stack direction="row" justifyContent="space-between" alignItems="center">
        <Typography variant="h5" fontWeight="bold">Districts</Typography>
        <Button variant="contained" onClick={startCreate}>Add District</Button>
      </Stack>

      <Paper sx={{ p: 2 }}>
        <Table size="small">
          <TableHead>
            <TableRow>
              <TableCell>ID</TableCell>
              <TableCell>State</TableCell>
              <TableCell>Name</TableCell>
              <TableCell>Active</TableCell>
              <TableCell align="right">Actions</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {items.map((d) => (
              <TableRow key={d.districtId}>
                <TableCell>{d.districtId}</TableCell>
                <TableCell>{stateNameById(d.stateId)}</TableCell>
                <TableCell>{d.name}</TableCell>
                <TableCell>{d.isActive ? "Yes" : "No"}</TableCell>
                <TableCell align="right">
                  <Stack direction="row" justifyContent="flex-end" gap={1}>
                    <Button size="small" onClick={() => startEdit(d)}>Edit</Button>
                    <Button size="small" color="error" onClick={() => onDelete(d.districtId)}>Delete</Button>
                  </Stack>
                </TableCell>
              </TableRow>
            ))}
            {items.length === 0 ? (
              <TableRow>
                <TableCell colSpan={5}>No districts found.</TableCell>
              </TableRow>
            ) : null}
          </TableBody>
        </Table>
      </Paper>

      <Dialog open={open} onClose={() => setOpen(false)} fullWidth maxWidth="sm">
        <DialogTitle>{form.districtId === 0 ? "Create District" : "Edit District"}</DialogTitle>
        <DialogContent sx={{ pt: 2 }}>
          <Stack gap={2} sx={{ mt: 1 }}>
            <TextField
              select
              label="State"
              value={form.stateId || ""}
              onChange={(e) => setForm((p) => ({ ...p, stateId: Number(e.target.value) }))}
              fullWidth
            >
              {states.map((s) => (
                <MenuItem key={s.id} value={s.id.trim() ? Number(s.id) : ""}>
                  {s.name}
                </MenuItem>
              ))}
            </TextField>

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

