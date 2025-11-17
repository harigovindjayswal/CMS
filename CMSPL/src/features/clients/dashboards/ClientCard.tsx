import {
  Box,
  Button,
  Card,
  CardActions,
  CardContent,
  Chip,
  Typography,
} from "@mui/material";
import { useClients } from "../../../lib/hooks/useClients";
import { Link } from "react-router";

type Prop = {
  clients: Client;
};
export default function ClientCard({ clients }: Prop) {
  const { deleteClient } = useClients();
  return (
    <>
      <Card sx={{ borderRadius: 3 }}>
        <CardContent>
          <Typography variant="h5">{clients.firstName}</Typography>
          <Typography sx={{ color: "text.secondry", mb: 1 }}>
            {clients.createdDate}
          </Typography>
          <Typography variant="body2">{clients.address}</Typography>
          <Typography variant="subtitle1">{clients.pinCode}</Typography>
        </CardContent>
        <CardActions>
          <Chip variant="outlined" label={clients.mobileNo} />
          <Box display={"flex"} gap={3}>
            <Button
             component={Link} to={`/clientDetails/${clients.clientId}`}
              size="medium"
              variant="contained"
            >
              View
            </Button>
            <Button
              onClick={() => deleteClient.mutate(clients.clientId)}
              size="medium"
              color="error"
              disabled={deleteClient.isPending}
              variant="contained"
            >
              Delete
            </Button>
          </Box>
        </CardActions>
      </Card>
    </>
  );
}
