import { Box, Button, Paper, TextField, Typography } from "@mui/material";
import type { FormEvent } from "react";
type Props = {
  client?: Client;
  closeForm: () => void;
  handleFormSubmit: (client: Client) => void;
};
export default function ClientForm({
  client,
  closeForm,
  handleFormSubmit,
}: Props) {
  const handleSubmit = (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const formData = new FormData(event.currentTarget);
    const data: { [key: string]: FormDataEntryValue } = {};
    formData.forEach((value, key) => {
      data[key] = value;
    });

    if (client) data.clientId = client.clientId.toString();
    handleFormSubmit(data as unknown as Client);
    
  };
  return (
    <Paper sx={{ borderRadius: 3, padding: 3 }}>
      <Typography variant="h5" gutterBottom color="primary">
        Create Client
      </Typography>
      <Box
        component="form"
        onSubmit={handleSubmit}
        display="flex"
        flexDirection="column"
        gap={3}
      >
        <TextField
          name="firstName"
          label="First Name"
          defaultValue={client?.firstName}
        />
        <TextField
          name="middleName"
          label="Middle Name"
          defaultValue={client?.middleName}
        />
        <TextField
          name="lastName"
          label="Last Name"
          defaultValue={client?.lastName}
        />
        <TextField
          name="emailId"
          label="Email"
          defaultValue={client?.emailId}
        />
        <TextField
          name="mobileNo"
          label="Mobile"
          defaultValue={client?.mobileNo}
        />
        <TextField
          name="address"
          label="Address"
          multiline
          rows={3}
          defaultValue={client?.address}
        />
        <TextField name="state" label="State" defaultValue={client?.state} />
        <TextField
          name="district"
          label="District"
          defaultValue={client?.district}
        />
        <TextField name="city" label="City" defaultValue={client?.city} />
        <TextField
          name="pinCode"
          label="Pin Code"
          defaultValue={client?.pinCode}
        />
        <TextField
          name="notes"
          label="Notes"
          multiline
          rows={3}
          defaultValue={client?.notes}
        />
        <TextField
          name="createdBy"
          label="Created By"
          defaultValue={client?.createdBy}
        />
        <TextField
          name="createdDate"
          label="Created Date"
          type="date"
          defaultValue={client?.createdDate}
        />
        <TextField
          name="updatedBy"
          label="Updated By"
          defaultValue={client?.updatedBy}
        />
        <TextField
          name="updatedDate"
          label="Updated Date"
          type="date"
          defaultValue={client?.updatedDate}
        />
        <TextField
          name="isActive"
          label="Is Active"
          defaultValue={client?.isActive}
        />
        <Box display="flex" justifyContent="end" gap={3}>
          <Button color="inherit" onClick={closeForm}>
            Cancel
          </Button>
          <Button type="submit" color="success" variant="contained">
            Submit
          </Button>
        </Box>
      </Box>
    </Paper>
  );
}
