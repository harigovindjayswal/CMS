import { zodResolver } from "@hookform/resolvers/zod";
import { Box, Button, Paper, Stack, Typography } from "@mui/material";
import { useForm } from "react-hook-form";
import { z } from "zod";
import TextInput from "../../../app/shared/components/TextInput";
import SelectInput from "../../../app/shared/components/SelectInput";
import { useManagedLawyers, useManagedStaff } from "../../../lib/hooks/useLawyerAdmin";
import { toast } from "react-toastify";

const schema = z.object({
  email: z.string().email(),
  displayName: z.string().min(2).max(120),
  password: z.string().min(6).max(100),
  lawyerId: z.string().min(1),
  firstName: z.string().max(50).optional().or(z.literal("")),
  middleName: z.string().max(50).optional().or(z.literal("")),
  lastName: z.string().max(50).optional().or(z.literal("")),
  mobileNo: z.string().max(20).optional().or(z.literal("")),
});

type FormValues = z.infer<typeof schema>;

export default function RegisterManagedStaffPage() {
  const { create } = useManagedStaff();
  const lawyers = useManagedLawyers();
  const items = (lawyers.list.data ?? []).map((l) => ({
    id: String(l.lawyerId),
    name: `${l.firstName ?? ""} ${l.lastName ?? ""}`.trim() || `Lawyer ${l.lawyerId}`,
  }));

  const { control, handleSubmit, formState: { isValid, isSubmitting }, reset } = useForm<FormValues>({
    mode: "onTouched",
    resolver: zodResolver(schema),
    defaultValues: {
      email: "",
      displayName: "",
      password: "",
      lawyerId: " ",
      firstName: "",
      middleName: "",
      lastName: "",
      mobileNo: "",
    },
  });

  const onSubmit = async (data: FormValues) => {
    const parsedLawyerId = data.lawyerId && data.lawyerId.trim() ? Number(data.lawyerId) : 0;

    await create.mutateAsync(
      {
        email: data.email.trim(),
        displayName: data.displayName.trim(),
        password: data.password,
        lawyerId: parsedLawyerId,
        firstName: data.firstName?.trim() || undefined,
        middleName: data.middleName?.trim() || undefined,
        lastName: data.lastName?.trim() || undefined,
        mobileNo: data.mobileNo?.trim() || undefined,
      },
      {
        onSuccess: () => {
          toast.success("Staff account created");
          reset();
        },
      }
    );
  };

  return (
    <Paper component="form" onSubmit={handleSubmit(onSubmit)} sx={{ p: 3, borderRadius: 3, maxWidth: 880 }}>
      <Stack gap={2}>
        <Box>
          <Typography variant="h5" fontWeight="bold">Register Staff</Typography>
          <Typography color="text.secondary">Creates a login account for a new Staff member under your LawyerAdmin scope.</Typography>
        </Box>

        <Stack direction={{ xs: "column", md: "row" }} gap={2}>
          <TextInput label="Email" control={control} name="email" />
          <TextInput label="Display Name" control={control} name="displayName" />
        </Stack>
        <TextInput label="Password" control={control} name="password" type="password" />

        <SelectInput
          items={[{ id: " ", name: "-- Select --" }, ...items]}
          label="Assign To Lawyer"
          control={control}
          name="lawyerId"
        />

        <Stack direction={{ xs: "column", md: "row" }} gap={2}>
          <TextInput label="First Name" control={control} name="firstName" />
          <TextInput label="Middle Name" control={control} name="middleName" />
          <TextInput label="Last Name" control={control} name="lastName" />
        </Stack>

        <TextInput label="Mobile" control={control} name="mobileNo" />

        <Button type="submit" variant="contained" disabled={!isValid} loading={isSubmitting}>
          Create Staff
        </Button>
      </Stack>
    </Paper>
  );
}

