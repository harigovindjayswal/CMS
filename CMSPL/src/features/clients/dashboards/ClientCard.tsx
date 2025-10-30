import {
  Box,
  Button,
  Card,
  CardActions,
  CardContent,
  Chip,
  Typography,
} from "@mui/material";

type Prop = {
  clients: Client;
  selectClient: (id:number)=>void;
  handleDelete:(id:number)=>void;
};
export default function ClientCard({ clients,selectClient,handleDelete }: Prop) {
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
          <Box display={'flex'} gap={3}>
            <Button onClick={()=>selectClient(clients.clientId)} size="medium" variant="contained">
            View
          </Button>
          <Button onClick={()=>handleDelete(clients.clientId)} size="medium" color="error" variant="contained">
            Delete
          </Button>
          </Box>
          
        </CardActions>
      </Card>
    </>
  );
}
