import { Button, Dialog, DialogActions, DialogContent, DialogTitle, FormControlLabel, MenuItem, Paper, Stack, Switch, Table, TableBody, TableCell, TableHead, TableRow, TextField, Typography } from "@mui/material";
import { useMemo, useState } from "react";
import { toast } from "react-toastify";
import { useCities, useDistricts } from "../../../../lib/hooks/useAdminMasterData";
import { useUtility } from "../../../../lib/hooks/useUtility";

type FormState = { cityId: number; stateId: number; districtId: number; name: string; isActive: boolean };

export default function CitiesPage() {
  const cities = useCities();
  const districts = useDistricts();
  const { optionLoader: states } = useUtility("State");

  const [open, setOpen] = useState(false);
  const [form, setForm] = useState<FormState>({ cityId: 0, stateId: 0, districtId: 0, name: "", isActive: true });

  const items = cities.list.data ?? [];
  const districtItems = districts.list.data ?? [];

  const districtNameById = (id: number) => districtItems.find((d) => d.districtId === id)?.name ?? String(id);
  const stateNameById = (id: number) => states.find((s) => Number(s.id) === id)?.name ?? String(id);

  const filteredDistricts = useMemo(() => {
    if (!form.stateId) return [];
    return districtItems.filter((d) => d.stateId === form.stateId);
  }, [districtItems, form.stateId]);

  const startCreate = () => {
    setForm({ cityId: 0, stateId: 0, districtId: 0, name: "", isActive: true });
    setOpen(true);
  };

  const startEdit = (item: CityMst) => {
    const district = districtItems.find((d) => d.districtId === item.districtId);
    setForm({
      cityId: item.cityId,
      stateId: district?.stateId ?? 0,
      districtId: item.districtId,
      name: item.name,
      isActive: item.isActive,
    });
    setOpen(true);
  };

  const onSave = async () => {
    if (!form.stateId) {
      toast.error("State is required");
      return;
    }
    if (!form.districtId) {
      toast.error("District is required");
      return;
    }
    if (!form.name.trim()) {
      toast.error("Name is required");
      return;
    }

    if (form.cityId === 0) {
      await cities.create.mutateAsync({ districtId: form.districtId, name: form.name.trim(), isActive: form.isActive });
      toast.success("City created");
    } else {
      await cities.update.mutateAsync({ cityId: form.cityId, districtId: form.districtId, name: form.name.trim(), isActive: form.isActive });
      toast.success("City updated");
    }
    setOpen(false);
  };

  const onDelete = async (id: number) => {
    await cities.remove.mutateAsync(id);
    toast.success("City deleted");
  };

  return (
    <Stack gap={2}>
      <Stack direction="row" justifyContent="space-between" alignItems="center">
        <Typography variant="h5" fontWeight="bold">Cities</Typography>
        <Button variant="contained" onClick={startCreate}>Add City</Button>
      </Stack>

      <Paper sx={{ p: 2 }}>
        <Table size="small">
          <TableHead>
            <TableRow>
              <TableCell>ID</TableCell>
              <TableCell>District</TableCell>
              <TableCell>Name</TableCell>
              <TableCell>Active</TableCell>
              <TableCell align="right">Actions</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {items.map((c) => (
              <TableRow key={c.cityId}>
                <TableCell>{c.cityId}</TableCell>
                <TableCell>{districtNameById(c.districtId)}</TableCell>
                <TableCell>{c.name}</TableCell>
                <TableCell>{c.isActive ? "Yes" : "No"}</TableCell>
                <TableCell align="right">
                  <Stack direction="row" justifyContent="flex-end" gap={1}>
                    <Button size="small" onClick={() => startEdit(c)}>Edit</Button>
                    <Button size="small" color="error" onClick={() => onDelete(c.cityId)}>Delete</Button>
                  </Stack>
                </TableCell>
              </TableRow>
            ))}
            {items.length === 0 ? (
              <TableRow>
                <TableCell colSpan={5}>No cities found.</TableCell>
              </TableRow>
            ) : null}
          </TableBody>
        </Table>
      </Paper>

      <Dialog open={open} onClose={() => setOpen(false)} fullWidth maxWidth="sm">
        <DialogTitle>{form.cityId === 0 ? "Create City" : "Edit City"}</DialogTitle>
        <DialogContent sx={{ pt: 2 }}>
          <Stack gap={2} sx={{ mt: 1 }}>
            <TextField
              select
              label="State"
              value={form.stateId || ""}
              onChange={(e) =>
                setForm((p) => ({ ...p, stateId: Number(e.target.value), districtId: 0 }))
              }
              fullWidth
            >
              {states.map((s) => (
                <MenuItem key={s.id} value={s.id.trim() ? Number(s.id) : ""}>
                  {s.name}
                </MenuItem>
              ))}
            </TextField>

            <TextField
              select
              label="District"
              value={form.districtId || ""}
              onChange={(e) => setForm((p) => ({ ...p, districtId: Number(e.target.value) }))}
              fullWidth
              disabled={!form.stateId}
              helperText={form.stateId ? "" : "Select a state first"}
            >
              {filteredDistricts.map((d) => (
                <MenuItem key={d.districtId} value={d.districtId}>
                  {d.name} ({stateNameById(d.stateId)})
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
          <Button variant="contained" onClick={onSave} disabled={cities.create.isPending || cities.update.isPending}>
            Save
          </Button>
        </DialogActions>
      </Dialog>
    </Stack>
  );
}

