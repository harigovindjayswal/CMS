import { useState } from "react";
import { Avatar, Box, Button, Divider, ListItemIcon, ListItemText, Menu, MenuItem } from "@mui/material";
import { Logout, Person, Work } from "@mui/icons-material";
import { Link } from "react-router";
import { useAccount } from "../../lib/hooks/useAccount";


export default function UserMenu() {
    const { currentUser, logoutUser } = useAccount();
    const userType = currentUser?.userType;
    const requestsPath = userType === 'Lawyer' ? '/incoming-requests' : '/my-requests';
    const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
    const open = Boolean(anchorEl);

    const handleClick = (event: React.MouseEvent<HTMLButtonElement>) => {
        setAnchorEl(event.currentTarget);
    };
    const handleClose = () => {
        setAnchorEl(null);
    };

    return (
        <>
            <Button
                onClick={handleClick}
                color='inherit'
                size="large"
                sx={{ fontSize: '1.1rem' }}
            >
                <Box display='flex' alignItems='center' gap={2} >
                    <Avatar />
                    {currentUser?.displayName}
                </Box>

            </Button>
            <Menu
                id="basic-menu"
                anchorEl={anchorEl}
                open={open}
                onClose={handleClose}
                slotProps={{
                    list: {
                        'aria-labelledby': 'basic-button'
                    }
                }}
            >
                {(userType === 'Client' || userType === 'Lawyer') ? (
                    <MenuItem component={Link} to={requestsPath} onClick={handleClose}>
                        <ListItemIcon>
                            <Work />
                        </ListItemIcon>
                        <ListItemText>Requests</ListItemText>
                    </MenuItem>
                ) : null}
                {(userType === 'Client' || userType === 'Lawyer') ? (
                    <MenuItem component={Link} to='/cases' onClick={handleClose}>
                        <ListItemIcon>
                            <Work />
                        </ListItemIcon>
                        <ListItemText>Cases</ListItemText>
                    </MenuItem>
                ) : null}
                {(userType === 'Client' || userType === 'Lawyer') ? (
                    <MenuItem component={Link} to='/profile' onClick={handleClose}>
                        <ListItemIcon>
                            <Person />
                        </ListItemIcon>
                        <ListItemText>My profile</ListItemText>
                    </MenuItem>
                ) : null}
                <Divider />
                <MenuItem onClick={() => {
                    logoutUser.mutate();
                    handleClose();
                }}>
                    <ListItemIcon>
                        <Logout />
                    </ListItemIcon>
                    <ListItemText>Logout</ListItemText>
                </MenuItem>
            </Menu>
        </>
    );
}
