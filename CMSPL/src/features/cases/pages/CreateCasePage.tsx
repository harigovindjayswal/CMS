import { Box, Button, Paper, Typography } from "@mui/material";
import { useMemo } from "react";
import { useParams, useNavigate } from "react-router";
import { useForm } from "react-hook-form";
import TextInput from "../../../app/shared/components/TextInput";
import SelectInput from "../../../app/shared/components/SelectInput";
import { useUtility } from "../../../lib/hooks/useUtility";
import { useCases } from "../../../lib/hooks/useCases";
import { toast } from "react-toastify";

type FormValues = {
  caseTypeId: string;
  courtName: string;
  title: string;
  description?: string;
  caseNumber?: string;
  purpose?: string;
};

export default function CreateCasePage() {
  const { lawyerRequestId } = useParams();
  const navigate = useNavigate();

  const requestId = Number(lawyerRequestId);
  const { createCase } = useCases("lawyer");
  const caseTypes = useUtility("CaseTypes");

  const { control, handleSubmit, formState } = useForm<FormValues>({
    mode: "onTouched",
    defaultValues: { caseTypeId: " ", courtName: "" },
  });

  const caseTypeItems = useMemo(() => caseTypes.optionLoader, [caseTypes.optionLoader]);

  const onSubmit = async (data: FormValues) => {
    if (!requestId || Number.isNaN(requestId)) {
      toast.error("Invalid lawyer request");
      return;
    }
    const parsedCaseTypeId = Number(data.caseTypeId);
    if (!parsedCaseTypeId) {
      toast.error("Please select a case type");
      return;
    }
    const created = await createCase.mutateAsync({
      lawyerRequestId: requestId,
      caseTypeId: parsedCaseTypeId,
      courtName: data.courtName,
      title: data.title,
      description: data.description,
      caseNumber: data.caseNumber,
      purpose: data.purpose,
    });
    navigate(`/cases/${created}`);
  };

  return (
    <Paper sx={{ p: 3, borderRadius: 3, maxWidth: "md", mx: "auto" }} component="form" onSubmit={handleSubmit(onSubmit)}>
      <Typography variant="h5" fontWeight="bold">
        Create case
      </Typography>
      <Box sx={{ display: "grid", gridTemplateColumns: "1fr 1fr", gap: 2, mt: 2 }}>
        <SelectInput label="Case type" name="caseTypeId" control={control} items={caseTypeItems} />
        <TextInput label="Court name" name="courtName" control={control} />
        <TextInput label="Title" name="title" control={control} />
        <TextInput label="Case number" name="caseNumber" control={control} />
        <TextInput label="Purpose" name="purpose" control={control} />
        <TextInput label="Description" name="description" control={control} multiline rows={3} />
      </Box>
      <Button sx={{ mt: 3 }} type="submit" variant="contained" size="large" disabled={!formState.isValid} loading={createCase.isPending}>
        Create
      </Button>
    </Paper>
  );
}

