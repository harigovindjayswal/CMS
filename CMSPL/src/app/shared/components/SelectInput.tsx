import { FormControl, FormHelperText, InputLabel, MenuItem, Select } from "@mui/material";
// import { SelectInputProps } from "@mui/material/Select/SelectInput";
import type { SelectInputProps } from "@mui/material/Select/SelectInput";
import { useController } from "react-hook-form"
import type { FieldValues, UseControllerProps } from "react-hook-form"

type Props<T extends FieldValues> = {
    items: {id: string, name: string}[];
    label: string;
} & UseControllerProps<T> & Partial<SelectInputProps>

export default function SelectInput<T extends FieldValues>(props: Props<T>) {
    const {field, fieldState} = useController({...props});

    return (
        <FormControl fullWidth error={!!fieldState.error}>
            <InputLabel>{props.label}</InputLabel>
            <Select
                value={field.value}
                label={props.label}
                onChange={field.onChange}
            >
                {props.items.map(item => (
                    <MenuItem key={item.id} value={item.id}>
                        {item.name}
                    </MenuItem>
                ))}
            </Select>
            <FormHelperText>{fieldState.error?.message}</FormHelperText>
        </FormControl>   
    )
}