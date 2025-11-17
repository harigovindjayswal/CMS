import {
  Button,
  Card,
  CardActions,
  CardContent,
  CardMedia,
  Typography,
} from "@mui/material";
import { useParams } from "react-router";
import { Link, useNavigate } from "react-router";
import { useClients } from "../../../lib/hooks/useClients";

export default function ClientDetails() {
  const navigate = useNavigate();

  const {id} = useParams();
  const { client, isClientLoading } = useClients(Number(id));

  if (isClientLoading) return <Typography>Loading...</Typography>;
  if (!client) return <Typography>Client Not Found</Typography>;
  return (
    <Card sx={{ borderRadius: 3 }}>
      <CardMedia
        component="img"
        src={`/images/categoryImages/${client.firstName}.jpg`}
      ></CardMedia>
      <CardContent>
        <Typography variant="h5">
          {client.firstName + " " + client.lastName}
        </Typography>
        <Typography variant="subtitle1">{client.address}</Typography>
        <Typography variant="body1">{client.pinCode}</Typography>
      </CardContent>
      <CardActions>
        <Button
          color="primary"
          component={Link}
          to={`/manage/${client.clientId}`}
        >
          Edit
        </Button>
        <Button onClick={() => navigate("/clients")} color="inherit">
          Cancel
        </Button>
      </CardActions>
    </Card>
  );
}
