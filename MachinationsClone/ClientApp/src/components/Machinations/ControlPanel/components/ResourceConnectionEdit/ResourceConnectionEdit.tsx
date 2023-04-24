import {EditComponentProps} from "../../ControlPanel.tsx";
import React, {FC} from "react";
import {FormControl, TextField} from "@mui/material";

export const ResourceConnectionEdit: FC<EditComponentProps> = ({ element, properties, setProperties }) => {
  const handleSpeedChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setProperties((p: Record<string, string>) =>
      ({...p, speed: event.target.value}));
  }

  return (
    <>
      <FormControl fullWidth margin="normal">
        <TextField required id="speed" label="Speed" value={properties.speed || element.properties.speed}
          onChange={handleSpeedChange} />
      </FormControl>
    </>
  )
}
