import { zodResolver } from "@hookform/resolvers/zod";
import { Box, Button, Paper, Stack, Typography } from "@mui/material";
import { useForm } from "react-hook-form";
import { z } from "zod";
import TextInput from "../../../app/shared/components/TextInput";
import { useManagedClients } from "../../../lib/hooks/useLawyerAdmin";
import { toast } from "react-toastify";

const schema = z.object({
  email: z.string().email(),
  displayName: z.string().min(2).max(120),
  password: z.string().min(6).max(100),
  firstName: z.string().max(50).optional().or(z.literal("")),
  middleName: z.string().max(50).optional().or(z.literal("")),
  lastName: z.string().max(50).optional().or(z.literal("")),
  mobileNo: z.string().max(20).optional().or(z.literal("")),
});

type FormValues = z.infer<typeof schema>;

export default function RegisterManagedClientPage() {
  const { create } = useManagedClients();
  const { control, handleSubmit, formState: { isValid, isSubmitting }, reset } = useForm<FormValues>({
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
    },
  });

  const onSubmit = async (data: FormValues) => {
    await create.mutateAsync(
      {
        ...data,
        firstName: data.firstName?.trim() || undefined,
        middleName: data.middleName?.trim() || undefined,
        lastName: data.lastName?.trim() || undefined,
        mobileNo: data.mobileNo?.trim() || undefined,
      },
      {
        onSuccess: () => {
          toast.success("Client account created");
          reset();
        },
      }
    );
  };

  return (
    <Paper component="form" onSubmit={handleSubmit(onSubmit)} sx={{ p: 3, borderRadius: 3, maxWidth: 760 }}>
      <Stack gap={2}>
        <Box>
          <Typography variant="h5" fontWeight="bold">Register Client</Typography>
          <Typography color="text.secondary">Creates a login account for a new Client under your LawyerAdmin scope.</Typography>
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

        <TextInput label="Mobile" control={control} name="mobileNo" />

        <Button type="submit" variant="contained" disabled={!isValid} loading={isSubmitting}>
          Create Client
        </Button>
      </Stack>
    </Paper>
  );
}

