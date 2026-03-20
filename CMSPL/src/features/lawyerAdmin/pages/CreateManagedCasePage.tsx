import { zodResolver } from "@hookform/resolvers/zod";
import { Box, Button, Paper, Stack, Typography } from "@mui/material";
import { useMemo } from "react";
import { useForm } from "react-hook-form";
import { z } from "zod";
import SelectInput from "../../../app/shared/components/SelectInput";
import TextInput from "../../../app/shared/components/TextInput";
import DateTimeInput from "../../../app/shared/components/DateTimeInput";
import { useManagedCases, useManagedClients, useManagedLawyers } from "../../../lib/hooks/useLawyerAdmin";
import { useUtility } from "../../../lib/hooks/useUtility";
import { toast } from "react-toastify";
import { useNavigate } from "react-router";

const schema = z.object({
  clientId: z.string().min(1),
  lawyerId: z.string().min(1),
  caseTypeId: z.string().min(1),
  title: z.string().min(3).max(200),
  courtName: z.string().min(2).max(200),
  caseNumber: z.string().max(100).optional().or(z.literal("")),
  purpose: z.string().max(300).optional().or(z.literal("")),
  description: z.string().max(2000).optional().or(z.literal("")),
  filingDate: z.any().optional(),
});

type FormValues = z.infer<typeof schema>;

export default function CreateManagedCasePage() {
  const navigate = useNavigate();
  const cases = useManagedCases();
  const clients = useManagedClients();
  const lawyers = useManagedLawyers();
  const { optionLoader: caseTypes } = useUtility("CaseTypes");

  const clientOptions = useMemo<OptionLoader[]>(() => {
    const items = clients.list.data ?? [];
    const mapped = items.map((c) => ({
      id: String(c.clientId),
      name: `${c.firstName ?? ""} ${c.lastName ?? ""}`.trim() || c.emailId || `Client #${c.clientId}`,
    }));
    return [{ id: " ", name: "-- Select --" }, ...mapped];
  }, [clients.list.data]);

  const lawyerOptions = useMemo<OptionLoader[]>(() => {
    const items = lawyers.list.data ?? [];
    const mapped = items.map((l) => ({
      id: String(l.lawyerId),
      name: `${l.firstName ?? ""} ${l.lastName ?? ""}`.trim() || l.emailId || `Lawyer #${l.lawyerId}`,
    }));
    return [{ id: " ", name: "-- Select --" }, ...mapped];
  }, [lawyers.list.data]);

  const { control, handleSubmit, formState: { isValid, isSubmitting } } = useForm<FormValues>({
    mode: "onTouched",
    resolver: zodResolver(schema),
    defaultValues: {
      clientId: " ",
      lawyerId: " ",
      caseTypeId: " ",
      title: "",
      courtName: "",
      caseNumber: "",
      purpose: "",
      description: "",
      filingDate: null,
    },
  });

  const onSubmit = async (data: FormValues) => {
    const payload = {
      clientId: Number(data.clientId),
      lawyerId: Number(data.lawyerId),
      caseTypeId: Number(data.caseTypeId),
      courtName: data.courtName.trim(),
      title: data.title.trim(),
      caseNumber: data.caseNumber?.trim() || undefined,
      purpose: data.purpose?.trim() || undefined,
      description: data.description?.trim() || undefined,
      filingDate: data.filingDate ? new Date(data.filingDate).toISOString() : null,
    };

    const id = await cases.createManual.mutateAsync(payload);
    toast.success("Case created");
    navigate(`/lawyeradmin/cases/${id}`);
  };

  return (
    <Paper component="form" onSubmit={handleSubmit(onSubmit)} sx={{ p: 3, borderRadius: 3, maxWidth: 980 }}>
      <Stack gap={2}>
        <Box>
          <Typography variant="h5" fontWeight="bold">Create Case</Typography>
          <Typography color="text.secondary">Creates a new case and assigns it to one of your managed lawyers.</Typography>
        </Box>

        <Stack direction={{ xs: "column", md: "row" }} gap={2}>
          <SelectInput items={clientOptions} label="Client" control={control} name="clientId" />
          <SelectInput items={lawyerOptions} label="Lawyer" control={control} name="lawyerId" />
        </Stack>

        <Stack direction={{ xs: "column", md: "row" }} gap={2}>
          <SelectInput items={caseTypes} label="Case Type" control={control} name="caseTypeId" />
          <TextInput label="Court Name" control={control} name="courtName" />
        </Stack>

        <Stack direction={{ xs: "column", md: "row" }} gap={2}>
          <TextInput label="Title" control={control} name="title" />
          <TextInput label="Case Number" control={control} name="caseNumber" />
        </Stack>

        <Stack direction={{ xs: "column", md: "row" }} gap={2}>
          <TextInput label="Purpose" control={control} name="purpose" />
          <DateTimeInput label="Filing Date" control={control} name="filingDate" />
        </Stack>

        <TextInput label="Description" control={control} name="description" multiline rows={4} />

        <Button type="submit" variant="contained" disabled={!isValid} loading={isSubmitting || cases.createManual.isPending}>
          Create Case
        </Button>
      </Stack>
    </Paper>
  );
}

