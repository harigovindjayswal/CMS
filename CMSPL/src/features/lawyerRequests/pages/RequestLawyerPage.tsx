import { zodResolver } from "@hookform/resolvers/zod";
import { Box, Button, Paper, Typography } from "@mui/material";
import { useMemo } from "react";
import { useForm } from "react-hook-form";
import SelectInput from "../../../app/shared/components/SelectInput";
import TextInput from "../../../app/shared/components/TextInput";
import { useUtility } from "../../../lib/hooks/useUtility";
import { useLawyerRequests } from "../../../lib/hooks/useLawyerRequests";
import { lawyerRequestSchema, type LawyerRequestSchema } from "../../../lib/schemas/lawyerRequestSchema";
import { useNavigate } from "react-router";

export default function RequestLawyerPage() {
  const navigate = useNavigate();
  const { createMutation } = useLawyerRequests("client");

  const { control, watch, handleSubmit, formState } = useForm<LawyerRequestSchema>({
    mode: "onTouched",
    resolver: zodResolver(lawyerRequestSchema),
    defaultValues: {
      caseTypeId: " ",
      stateId: " ",
      districtId: " ",
      cityId: " ",
      lawyerId: " ",
      caseDescription: "",
    },
  });

  const stateId = watch("stateId");
  const districtId = watch("districtId");
  const cityId = watch("cityId");

  const caseTypes = useUtility("CaseTypes");
  const states = useUtility("State");
  const districts = useUtility("District", stateId && stateId.trim() !== "" ? stateId : undefined);
  const cities = useUtility("City", districtId && districtId.trim() !== "" ? districtId : undefined);
  const lawyers = useUtility("Lawyers", cityId && cityId.trim() !== "" ? cityId : undefined);

  const caseTypeItems = useMemo(() => caseTypes.optionLoader, [caseTypes.optionLoader]);
  const stateItems = useMemo(() => states.optionLoader, [states.optionLoader]);
  const districtItems = useMemo(() => districts.optionLoader, [districts.optionLoader]);
  const cityItems = useMemo(() => cities.optionLoader, [cities.optionLoader]);
  const lawyerItems = useMemo(() => lawyers.optionLoader, [lawyers.optionLoader]);

  const onSubmit = async (data: LawyerRequestSchema) => {
    await createMutation.mutateAsync({
      lawyerId: Number(data.lawyerId),
      caseTypeId: Number(data.caseTypeId),
      stateId: Number(data.stateId),
      districtId: Number(data.districtId),
      cityId: Number(data.cityId),
      caseDescription: data.caseDescription,
    });
    navigate("/my-requests");
  };

  return (
    <Paper
      component="form"
      onSubmit={handleSubmit(onSubmit)}
      sx={{ p: 3, borderRadius: 3, maxWidth: "md", mx: "auto" }}
    >
      <Typography variant="h5" fontWeight="bold">
        Request a lawyer
      </Typography>
      <Box sx={{ display: "grid", gridTemplateColumns: "1fr 1fr", gap: 2, mt: 2 }}>
        <SelectInput label="Case type" name="caseTypeId" control={control} items={caseTypeItems} />
        <SelectInput label="State" name="stateId" control={control} items={stateItems} />
        <SelectInput label="District" name="districtId" control={control} items={districtItems} />
        <SelectInput label="City" name="cityId" control={control} items={cityItems} />
        <SelectInput label="Lawyer" name="lawyerId" control={control} items={lawyerItems} />
        <TextInput label="Case description" name="caseDescription" control={control} multiline rows={4} />
      </Box>
      <Button sx={{ mt: 3 }} type="submit" variant="contained" size="large" disabled={!formState.isValid} loading={createMutation.isPending}>
        Submit request
      </Button>
    </Paper>
  );
}

