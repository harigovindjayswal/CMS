import { useController } from "react-hook-form";
import type { FieldValues, UseControllerProps } from "react-hook-form";
import { DateTimePicker } from "@mui/x-date-pickers";
import type { DateTimePickerProps } from "@mui/x-date-pickers";

type Props<T extends FieldValues> =
  UseControllerProps<T> & DateTimePickerProps;

export default function DateTimeInput<T extends FieldValues>(props: Props<T>) {
  const { field, fieldState } = useController(props);

  return (
    <DateTimePicker
      {...props}
      value={field.value ? new Date(field.value) : null}
      onChange={(value) => {
        field.onChange(value);
      }}
      sx={{ width: "100%" }}
      slotProps={{
        textField: {
          onBlur: field.onBlur,
          error: !!fieldState.error,
          helperText: fieldState.error?.message,
        },
      }}
    />
  );
}
