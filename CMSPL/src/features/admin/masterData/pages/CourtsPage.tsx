import { Button, Dialog, DialogActions, DialogContent, DialogTitle, FormControlLabel, MenuItem, Paper, Stack, Switch, Table, TableBody, TableCell, TableHead, TableRow, TextField, Typography } from "@mui/material";
import { useMemo, useState } from "react";
import { toast } from "react-toastify";
import { useCities, useCourtTypes, useCourts, useDistricts } from "../../../../lib/hooks/useAdminMasterData";
import { useUtility } from "../../../../lib/hooks/useUtility";

type FormState = {
  courtId: number;
  courtTypeId: number;
  stateId: number;
  districtId: number;
  cityId: number;
  name: string;
  isActive: boolean;
};

export default function CourtsPage() {
  const courts = useCourts();
  const courtTypes = useCourtTypes();
  const cities = useCities();
  const districts = useDistricts();

  const [open, setOpen] = useState(false);
  const [form, setForm] = useState<FormState>({
    courtId: 0,
    courtTypeId: 0,
    stateId: 0,
    districtId: 0,
    cityId: 0,
    name: "",
    isActive: true,
  });

  const { optionLoader: stateOptions } = useUtility("State");
  const { optionLoader: districtOptions } = useUtility("District", form.stateId || undefined);
  const { optionLoader: cityOptions } = useUtility("City", form.districtId || undefined);

  const items = courts.list.data ?? [];
  const cityItems = cities.list.data ?? [];
  const districtItems = districts.list.data ?? [];
  const courtTypeItems = courtTypes.list.data ?? [];

  const courtTypeNameById = (id: number) => courtTypeItems.find((ct) => ct.courtTypeId === id)?.typeName ?? String(id);
  const cityNameById = (id: number) => cityItems.find((c) => c.cityId === id)?.name ?? String(id);

  const stateIdByDistrictId = useMemo(() => {
    const map = new Map<number, number>();
    for (const d of districtItems) map.set(d.districtId, d.stateId);
    return map;
  }, [districtItems]);

  const districtIdByCityId = useMemo(() => {
    const map = new Map<number, number>();
    for (const c of cityItems) map.set(c.cityId, c.districtId);
    return map;
  }, [cityItems]);

  const startCreate = () => {
    setForm({
      courtId: 0,
      courtTypeId: 0,
      stateId: 0,
      districtId: 0,
      cityId: 0,
      name: "",
      isActive: true,
    });
    setOpen(true);
  };

  const startEdit = (item: CourtMst) => {
    const districtId = districtIdByCityId.get(item.cityId) ?? 0;
    const stateId = stateIdByDistrictId.get(districtId) ?? 0;
    setForm({
      courtId: item.courtId,
      courtTypeId: item.courtTypeId,
      stateId,
      districtId,
      cityId: item.cityId,
      name: item.name,
      isActive: item.isActive,
    });
    setOpen(true);
  };

  const onSave = async () => {
    if (!form.courtTypeId) {
      toast.error("Court type is required");
      return;
    }
    if (!form.cityId) {
      toast.error("City is required");
      return;
    }
    if (!form.name.trim()) {
      toast.error("Name is required");
      return;
    }

    if (form.courtId === 0) {
      await courts.create.mutateAsync({
        courtTypeId: form.courtTypeId,
        cityId: form.cityId,
        name: form.name.trim(),
        isActive: form.isActive,
      });
      toast.success("Court created");
    } else {
      await courts.update.mutateAsync({
        courtId: form.courtId,
        courtTypeId: form.courtTypeId,
        cityId: form.cityId,
        name: form.name.trim(),
        isActive: form.isActive,
      });
      toast.success("Court updated");
    }

    setOpen(false);
  };

  const onDelete = async (id: number) => {
    await courts.remove.mutateAsync(id);
    toast.success("Court deleted");
  };

  return (
    <Stack gap={2}>
      <Stack direction="row" justifyContent="space-between" alignItems="center">
        <Typography variant="h5" fontWeight="bold">Courts</Typography>
        <Button variant="contained" onClick={startCreate}>Add Court</Button>
      </Stack>

      <Paper sx={{ p: 2 }}>
        <Table size="small">
          <TableHead>
            <TableRow>
              <TableCell>ID</TableCell>
              <TableCell>Court Type</TableCell>
              <TableCell>City</TableCell>
              <TableCell>Name</TableCell>
              <TableCell>Active</TableCell>
              <TableCell align="right">Actions</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {items.map((c) => (
              <TableRow key={c.courtId}>
                <TableCell>{c.courtId}</TableCell>
                <TableCell>{courtTypeNameById(c.courtTypeId)}</TableCell>
                <TableCell>{cityNameById(c.cityId)}</TableCell>
                <TableCell>{c.name}</TableCell>
                <TableCell>{c.isActive ? "Yes" : "No"}</TableCell>
                <TableCell align="right">
                  <Stack direction="row" justifyContent="flex-end" gap={1}>
                    <Button size="small" onClick={() => startEdit(c)}>Edit</Button>
                    <Button size="small" color="error" onClick={() => onDelete(c.courtId)}>Delete</Button>
                  </Stack>
                </TableCell>
              </TableRow>
            ))}
            {items.length === 0 ? (
              <TableRow>
                <TableCell colSpan={6}>No courts found.</TableCell>
              </TableRow>
            ) : null}
          </TableBody>
        </Table>
      </Paper>

      <Dialog open={open} onClose={() => setOpen(false)} fullWidth maxWidth="sm">
        <DialogTitle>{form.courtId === 0 ? "Create Court" : "Edit Court"}</DialogTitle>
        <DialogContent sx={{ pt: 2 }}>
          <Stack gap={2} sx={{ mt: 1 }}>
            <TextField
              select
              label="Court Type"
              value={form.courtTypeId || ""}
              onChange={(e) => setForm((p) => ({ ...p, courtTypeId: Number(e.target.value) }))}
              fullWidth
            >
              <MenuItem value="">-- Select --</MenuItem>
              {courtTypeItems.map((ct) => (
                <MenuItem key={ct.courtTypeId} value={ct.courtTypeId}>
                  {ct.typeName}
                </MenuItem>
              ))}
            </TextField>

            <TextField
              select
              label="State"
              value={form.stateId || ""}
              onChange={(e) =>
                setForm((p) => ({
                  ...p,
                  stateId: Number(e.target.value),
                  districtId: 0,
                  cityId: 0,
                }))
              }
              fullWidth
            >
              {stateOptions.map((s) => (
                <MenuItem key={s.id} value={s.id.trim() ? Number(s.id) : ""}>
                  {s.name}
                </MenuItem>
              ))}
            </TextField>

            <TextField
              select
              label="District"
              value={form.districtId || ""}
              onChange={(e) =>
                setForm((p) => ({
                  ...p,
                  districtId: Number(e.target.value),
                  cityId: 0,
                }))
              }
              fullWidth
              disabled={!form.stateId}
              helperText={form.stateId ? "" : "Select a state first"}
            >
              <MenuItem value="">-- Select --</MenuItem>
              {districtOptions
                .filter((d) => d.id.trim() !== "")
                .map((d) => (
                  <MenuItem key={d.id} value={Number(d.id)}>
                    {d.name}
                  </MenuItem>
                ))}
            </TextField>

            <TextField
              select
              label="City"
              value={form.cityId || ""}
              onChange={(e) => setForm((p) => ({ ...p, cityId: Number(e.target.value) }))}
              fullWidth
              disabled={!form.districtId}
              helperText={form.districtId ? "" : "Select a district first"}
            >
              <MenuItem value="">-- Select --</MenuItem>
              {cityOptions
                .filter((c) => c.id.trim() !== "")
                .map((c) => (
                  <MenuItem key={c.id} value={Number(c.id)}>
                    {c.name}
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
          <Button
            variant="contained"
            onClick={onSave}
            disabled={courts.create.isPending || courts.update.isPending}
          >
            Save
          </Button>
        </DialogActions>
      </Dialog>
    </Stack>
  );
}
