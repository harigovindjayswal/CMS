import { zodResolver } from "@hookform/resolvers/zod";
import { Box, Button, Paper, Stack, Typography } from "@mui/material";
import { useForm } from "react-hook-form";
import { z } from "zod";
import TextInput from "../../../app/shared/components/TextInput";
import SelectInput from "../../../app/shared/components/SelectInput";
import { useManagedLawyers } from "../../../lib/hooks/useLawyerAdmin";
import { toast } from "react-toastify";
import { useUtility } from "../../../lib/hooks/useUtility";

const schema = z.object({
  email: z.string().email(),
  displayName: z.string().min(2).max(120),
  password: z.string().min(6).max(100),
  firstName: z.string().max(50).optional().or(z.literal("")),
  middleName: z.string().max(50).optional().or(z.literal("")),
  lastName: z.string().max(50).optional().or(z.literal("")),
  mobileNo: z.string().max(20).optional().or(z.literal("")),
  stateId: z.string().optional(),
  districtId: z.string().optional(),
  cityId: z.string().optional(),
  barLicenseNumber: z.string().max(80).optional().or(z.literal("")),
});

type FormValues = z.infer<typeof schema>;

export default function RegisterManagedLawyerPage() {
  const { create } = useManagedLawyers();
  const { control, handleSubmit, watch, formState: { isValid, isSubmitting }, reset } = useForm<FormValues>({
    mode: "onTouched",
    resolver: zodResolver(schema),
    defaultValues: {
      email: "",
      displayName: "",
      password: "",
      firstName: "",
      middleName: "",
      lastName: "",
      mobileNo: "",
      stateId: " ",
      districtId: " ",
      cityId: " ",
      barLicenseNumber: "",
    },
  });

  const stateId = watch("stateId");
  const districtId = watch("districtId");

  const { optionLoader: states } = useUtility("State");
  const { optionLoader: districts } = useUtility("District", stateId && stateId.trim() ? Number(stateId) : undefined);
  const { optionLoader: cities } = useUtility("City", districtId && districtId.trim() ? Number(districtId) : undefined);

  const onSubmit = async (data: FormValues) => {
    const parsedStateId = data.stateId && data.stateId.trim() ? Number(data.stateId) : null;
    const parsedCityId = data.cityId && data.cityId.trim() ? Number(data.cityId) : null;

    await create.mutateAsync(
      {
        email: data.email.trim(),
        displayName: data.displayName.trim(),
        password: data.password,
        firstName: data.firstName?.trim() || undefined,
        middleName: data.middleName?.trim() || undefined,
        lastName: data.lastName?.trim() || undefined,
        mobileNo: data.mobileNo?.trim() || undefined,
        stateId: parsedStateId,
        cityId: parsedCityId,
        barLicenseNumber: data.barLicenseNumber?.trim() || undefined,
      },
      {
        onSuccess: () => {
          toast.success("Lawyer account created");
          reset();
        },
      }
    );
  };

  return (
    <Paper component="form" onSubmit={handleSubmit(onSubmit)} sx={{ p: 3, borderRadius: 3, maxWidth: 880 }}>
      <Stack gap={2}>
        <Box>
          <Typography variant="h5" fontWeight="bold">Register Lawyer</Typography>
          <Typography color="text.secondary">Creates a login account for a new Lawyer under your LawyerAdmin scope.</Typography>
        </Box>

        <Stack direction={{ xs: "column", md: "row" }} gap={2}>
          <TextInput label="Email" control={control} name="email" />
          <TextInput label="Display Name" control={control} name="displayName" />
        </Stack>
        <TextInput label="Password" control={control} name="password" type="password" />

        <Stack direction={{ xs: "column", md: "row" }} gap={2}>
          <TextInput label="First Name" control={control} name="firstName" />
          <TextInput label="Middle Name" control={control} name="middleName" />
          <TextInput label="Last Name" control={control} name="lastName" />
        </Stack>

        <Stack direction={{ xs: "column", md: "row" }} gap={2}>
          <TextInput label="Mobile" control={control} name="mobileNo" />
          <TextInput label="Bar License Number" control={control} name="barLicenseNumber" />
        </Stack>

        <Stack direction={{ xs: "column", md: "row" }} gap={2}>
          <SelectInput items={states} label="State" control={control} name="stateId" />
          <SelectInput items={districts} label="District" control={control} name="districtId" disabled={!stateId || stateId === " "} />
          <SelectInput items={cities} label="City" control={control} name="cityId" disabled={!districtId || districtId === " "} />
        </Stack>

        <Button type="submit" variant="contained" disabled={!isValid} loading={isSubmitting}>
          Create Lawyer
        </Button>
      </Stack>
    </Paper>
  );
}

