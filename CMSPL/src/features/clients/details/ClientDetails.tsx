import {
  Button,
  Card,
  CardActions,
  CardContent,
  CardMedia,
  Typography,
} from "@mui/material";
type Props={
    client:Client;
    cancelSelectClient:()=>void;
    openForm:(id:number)=>void;
}
export default function ClientDetails({client,cancelSelectClient,openForm}:Props) {
  return (
    <Card sx={{ borderRadius: 3 }}>
      <CardMedia component="img" src={`/images/categoryImages/${client.firstName}.jpg`}></CardMedia>
      <CardContent>
        <Typography variant="h5">{client.firstName +' '+ client.lastName}</Typography>
        <Typography variant="subtitle1">{client.address}</Typography>
        <Typography variant="body1">{client.pinCode}</Typography>
      </CardContent>
      <CardActions>
        <Button color="primary" onClick={()=>openForm(client.clientId)}>Edit</Button>
        <Button onClick={()=>cancelSelectClient()} color="inherit">Cancel</Button>
      </CardActions>
    </Card>
  );
}
