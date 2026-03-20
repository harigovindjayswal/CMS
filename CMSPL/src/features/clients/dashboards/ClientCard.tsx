import { AccessTime, Place } from "@mui/icons-material";
import {
  Avatar,
  Box,
  Button,
  Card,
  CardContent,
  CardHeader,
  Chip,
  Divider,
  Typography,
} from "@mui/material";
import { Link } from "react-router";
import { formatDate } from "../../../lib/util/util";

type Prop = {
  clients: Client;
};
export default function ClientCard({ clients }: Prop) {
  const isActive = false;
  const isFinished = false;
  const isCancelled = false;
  const label = isActive ? "Client is Active" : "Client is inactive";
  const color = isActive ? "secondary" : isFinished ? "warning" : "default";

  return (
    <>
      <Card elevation={3} sx={{ borderRadius: 3 }}>
        <Box
          display={"flex"}
          justifyContent={"space-between"}
          alignItems={"center"}
        >
          <CardHeader
            title={clients.firstName}
            avatar={<Avatar sx={{ height: 80, width: 80 }} />}
            slotProps={{
              fontWeight: "bold",
              fontSize: 20,
            }}
            subheader={
              <>
                Hosted By <Link to={`/profiles/bob`}>Bob </Link>
              </>
            }
          />
          <Box display={"flex"} flexDirection={"column"} gap={2} mr={2}>
            {(isActive || isFinished) && (
              <Chip label={label} color={color} sx={{ borderRadius: 2 }} />
            )}
            {isCancelled && (
              <Chip label="Cancelled" color="error" sx={{ borderRadius: 2 }} />
            )}
          </Box>
        </Box>
        <Divider sx={{ mb: 3 }} />

        <CardContent sx={{ p: 0 }}>
          <Box display={"flex"} alignItems={"center"} mb={2} px={2}>
            <AccessTime sx={{ mr: 1 }} />
            <Typography variant="body2">
              {clients.createdDate ? formatDate(clients.createdDate) : "-"}
            </Typography>
            <Place sx={{ ml: 3, mr: 1 }} />
            <Typography variant="body2">{clients.address}</Typography>
          </Box>
          <Divider />
          <Box
            display={"flex"}
            gap={2}
            sx={{ backgroundColor: "grey.200", py: 3, pl: 3 }}
          >
            Attendeis go here
          </Box>
        </CardContent>
        <CardContent sx={{ pb: 2 }}>
          <Typography variant="body2">{clients.emailId}</Typography>
          <Button
            component={Link}
            to={`/clientDetails/${clients.clientId}`}
            size="medium"
            variant="contained"
            sx={{ display: "flex", justifySelf: "self-end", borderRadius: 3 }}
          >
            View
          </Button>
        </CardContent>
      </Card>
    </>
  );
}
