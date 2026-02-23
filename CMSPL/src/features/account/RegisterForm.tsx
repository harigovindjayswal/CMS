import { useAccount } from "../../lib/hooks/useAccount.ts";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { Box, Button, Paper, Typography } from "@mui/material";
import { LockOpen } from "@mui/icons-material";
import TextInput from "../../app/shared/components/TextInput.tsx";
import {
  registerSchema,
  type RegisterSchema,
} from "../../lib/schemas/registerSchema.ts";
import { Link, useLocation, useNavigate } from "react-router";
import SelectInput from "../../app/shared/components/SelectInput.tsx";
import { useUtility } from "../../lib/hooks/useUtility.ts";
export default function RegisterForm() {
  const { registerUser } = useAccount();
  const navigate = useNavigate();
  const location = useLocation();
  const {isLoading,optionLoader}=useUtility("1");
  const {
    control,
    handleSubmit,
    setError,
    formState: { isValid, isSubmitting },
  } = useForm<RegisterSchema>({
    mode: "onTouched",
    resolver: zodResolver(registerSchema),
    defaultValues:{
      userType:" "
    }
  });

  const onSubmit = async (data: RegisterSchema) => {
    console.log(data);
    await registerUser.mutateAsync(data, {
      onSuccess: () => {
                navigate(location.state?.from || '/login')
            },
      onError: (error) => {
        if (Array.isArray(error)) {
          error.forEach((err) => {
            if (err.includes("Email")) setError("email", { message: err });
            else if (err.includes("Password"))
              setError("password", { message: err });
          });
        }
      }
    });
  };

  return (
    <Paper
      component="form"
      onSubmit={handleSubmit(onSubmit)}
      sx={{
        display: "flex",
        flexDirection: "column",
        p: 3,
        gap: 3,
        maxWidth: "md",
        mx: "auto",
        borderRadius: 3,
      }}
    >
      <Box
        display="flex"
        alignItems="center"
        justifyContent="center"
        gap={3}
        color="secondary.main"
      >
        <LockOpen fontSize="large" />
        <Typography variant="h4">Register</Typography>
      </Box>
      <TextInput label="Email" control={control} name="email" />
      <TextInput label="Display name" control={control} name="displayName" />
      <TextInput
        label="Password"
        control={control}
        name="password"
        type="password"
      />

      <SelectInput
        items={optionLoader}
        label="User Type"
        control={control}
        name="userType"
        disabled={isLoading}
      />

      <Button
        type="submit"
        loading={isSubmitting}
        disabled={!isValid || isSubmitting}
        variant="contained"
        size="large"
      >
        Register
      </Button>
      <Typography sx={{ textAlign: "center" }}>
        Already have an account?
        <Typography sx={{ ml: 2 }} component={Link} to="/login" color="primary">
          Sign in
        </Typography>
      </Typography>
    </Paper>
  );
}
