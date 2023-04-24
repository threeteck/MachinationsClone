import {EditComponentProps} from "../../ControlPanel.tsx";
import React, {FC} from "react";
import {FormControl, TextField} from "@mui/material";

export const PoolEdit: FC<EditComponentProps> = ({ element, properties, setProperties }) => {
  const handleStartAmountChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setProperties((p: Record<string, string>) =>
      ({...p, startAmount: event.target.value}));
  }

  const handleCapacityChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setProperties((p: Record<string, string>) =>
      ({...p, capacity: event.target.value}));
  }

  return (
    <>
      <FormControl fullWidth margin="normal">
        <TextField required id="startAmount" label="Start Amount" value={properties.startAmount || element.properties.startAmount}
          onChange={handleStartAmountChange} />
      </FormControl>
      <FormControl fullWidth margin="normal">
        <TextField required id="capacity" label="Capacity" value={properties.capacity || element.properties.capacity}
          onChange={handleCapacityChange} />
      </FormControl>
    </>
  )
}
